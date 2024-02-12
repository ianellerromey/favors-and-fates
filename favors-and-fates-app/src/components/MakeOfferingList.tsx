import React, { useState } from 'react';
import './styles/MakeOfferingList.css';
import { MakeOffering } from './MakeOffering.tsx';
import { FavorEngine } from '../engines/FavorEngine.ts';
import { FavorRequested, TokenOffered } from '../types/index.ts';

type Params = {
  favorRequested: FavorRequested;
};

export function MakeOfferingList(params: Params) {
  const { favorRequested } = params;
  const [ tokensOffered, setTokensOffered ] = useState([] as TokenOffered[]);
  
  FavorEngine.currentTokensOffered.subscribe(currentTokensOffered => {
    setTokensOffered(currentTokensOffered);
  });

  const submitFavorResponse = () => {
    FavorEngine.getFateFromFavorResponse();
  };

  return (
    <div className='makeOfferingList'>
      <h1>OFFERINGS</h1>
      <h3>
        {favorRequested?.name}
      </h3>
      <div>
        {(favorRequested?.tokensRequested || []).map((token, index) => {
          return <MakeOffering key={index} token={token} />;
        })}
      </div>
      {(favorRequested && tokensOffered.length === favorRequested.tokensRequested.length) ? <div><button onClick={submitFavorResponse}>Submit</button></div> : <div/>}
    </div>
  );
}