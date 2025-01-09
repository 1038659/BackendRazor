import React from 'react';
import App from './App';
import ReactDOM from 'react-dom';


export const main = () => {

  const rootElement =document.querySelector('#root')

  ReactDOM.render(
      <App/>,
      rootElement
  )
}



