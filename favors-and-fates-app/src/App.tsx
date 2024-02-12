import React, { useEffect, useState } from 'react';
import { Fate, FavorRequested } from './types';
import { ReceiveFate } from './components/ReceiveFate.tsx';
import { MakeOfferingList } from './components/MakeOfferingList.tsx';
import { FavorEngine } from './engines/FavorEngine.ts';

function App() {
  let [ favorRequested, setFavorRequested ] = useState(null as any);
  let [ fateRecieved, setFateRecieved ] = useState(null as any);

  const updateFavorRequested = (favorRequested: FavorRequested) => {
    FavorEngine.setFavorRequested(favorRequested);
    setFavorRequested(favorRequested);
  };

  const getLatestFavorRequest = () => {
    FavorEngine.getLatestFavorRequest()
      .then((response: FavorRequested) => {
        updateFavorRequested(response);
      });
  };

  useEffect(() => {
    getLatestFavorRequest();
  }, []);

  FavorEngine.currentFateReceived.subscribe((fate: Fate) => setFateRecieved(fate));

  return (
    <div>
      {!fateRecieved && <MakeOfferingList favorRequested={favorRequested}/>}
      {fateRecieved && <ReceiveFate fate={fateRecieved}/>}
    </div>
  );
}

export default App;
