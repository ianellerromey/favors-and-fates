import { TokenRequested } from './TokenRequested';

export interface FavorRequested {
  id: string;
  name: string;
  tokensRequested: TokenRequested[];
};