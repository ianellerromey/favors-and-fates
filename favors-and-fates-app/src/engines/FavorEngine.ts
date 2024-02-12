import { Subject } from 'rxjs';
import * as settings from '../appSettings.json';
import { Fate, FavorRequested, FavorResponse, TokenOffered, TokenRequested } from '../types';

const { favorsAndFatesServiceUrl } = settings;
const currentTokensOffered = new Map<string, TokenOffered>();
let currentFavorRequested = {} as FavorRequested;
let currentFateReceived = {} as Fate;
const favorRequestedSubject = new Subject<FavorRequested>();
const tokenOfferedSubject = new Subject<TokenOffered[]>();
const fateRecievedSubject = new Subject<Fate>();

export const FavorEngine = {
  setFavorRequested: (favorRequested: FavorRequested) => {
    currentFavorRequested = favorRequested;
    favorRequestedSubject.next(currentFavorRequested);
  },
  currentFavorRequested: favorRequestedSubject,
  setTokenOffered: (tokenRequested: TokenRequested, tokenOffered: TokenOffered) => {
    currentTokensOffered.set(tokenRequested.name, tokenOffered);
    tokenOfferedSubject.next([...currentTokensOffered].map((value: [string, TokenOffered]) => value[1]));
  },
  currentTokensOffered: tokenOfferedSubject,
  setFateRecieved: (fateReceived: Fate) => {
    currentFateReceived = fateReceived;
    fateRecievedSubject.next(currentFateReceived);
  },
  currentFateReceived: fateRecievedSubject,
  getLatestFavorRequest: async (): Promise<FavorRequested> => {
    return new Promise((resolve) =>
    {
      fetch(favorsAndFatesServiceUrl,
        {
          method: 'GET'
        }).then((response: any) => {
            response.json().then((body: any) => {
              resolve(body as FavorRequested);
            });
        });
    });
  },
  getFateFromFavorResponse: async (): Promise<Fate> => {
    return new Promise((resolve) =>
    {
      const cto = [...currentTokensOffered];
      const favorResponse = {
        id: currentFavorRequested.id,
        tokensOffered: cto.map((value: [string, TokenOffered]) => value[1].name)
      } as FavorResponse;

      const formData = new FormData();

      formData.append('favorResponse', JSON.stringify(favorResponse));
      cto.forEach((value: [string, TokenOffered]) => {
        formData.append('file', value[1].value);
      });

      fetch(favorsAndFatesServiceUrl,
        {
          method: 'POST',
          body: formData
        }).then((response: any) => {
          response.json().then((body: any) => {
            const fate = body as Fate;
            resolve(fate);
            FavorEngine.setFateRecieved(fate);
          });
      });
    });
  }
};