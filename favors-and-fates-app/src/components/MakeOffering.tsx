import React, { useState } from 'react';
import './styles/MakeOffering.css';
import { TokenRequested } from '../types';
import { FavorEngine } from '../engines/FavorEngine.ts';

type Params = {
  token: TokenRequested;
};

export function MakeOffering(params: Params) {
  const { token } = params;
  let [ file, setFile ] = useState({} as File);
  
  const updateOffering = (file: File) => {
    FavorEngine.setTokenOffered(token, {
      name: token.name,
      value: file
    });
    setFile(file);
  };

  const openFileDialog = () => {
    const input = document.createElement('input');
    input.type = 'file';
    input.onchange = (event: Event) => {
      const [ file ] = input.files || [];
      updateOffering(file);
      input.remove();
    };
    input.click();
  };

  return (
    <div className='makeOffering'>
      <div>
        <span className='makeOfferingName'>
          {token.name}
        </span>
        <button onClick={openFileDialog}>
          Offer Token
        </button>
      </div>
      <div>
      {
        file.name && <img className='makeOfferingImage' alt='token offered' src={URL.createObjectURL(file)} />
      }
      </div>
    </div>
  );
};