import React from 'react';
import { Home } from './Home/Home';
import ReactDOM from 'react-dom';


export const main = () => {

  const rootElement =document.querySelector('#root')

  ReactDOM.render(
      <Home/>,
      rootElement
  )
}
