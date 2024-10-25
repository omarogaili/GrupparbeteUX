import React from 'react';
import Header from './components/Header/Header';
import Body from './components/Body/Body';
import Footer from './components/Footer/Footer';
import './styles.scss'; 

const App = () => {
  return (
    <div className="parent">
      <Header />
      <Body />
      <Footer />
    </div>
  );
};

export default App;
