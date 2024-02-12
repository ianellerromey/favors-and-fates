import React from 'react';
import './styles/ReceiveFate.css';
import { Fate } from '../types/index.ts';

type Params = {
  fate: Fate;
};

export function ReceiveFate(params: Params) {
  const { fate } = params;

  return (
    <div className='receiveFate'>
      <h1>FATE</h1>
      <h3>
        {fate.name}
      </h3>
      <div className='receiveFateText'>
        {fate.value}
      </div>
    </div>
  );
}