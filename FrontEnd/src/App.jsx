import React, { useState } from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import Home from './Pages/Home';
import ViewItem from './Pages/ViewItem';
import AddItem from './Pages/AddItem';
import About from './Pages/About';
import AuthModal from './components/LogSingIn/AuthModal';

const App = () => {

  return (
    <Router>
      <div>
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/annonser" element={<ViewItem />} />
          <Route path="/mina-annonser/lagg-in-annons" element={<AddItem />} />
          <Route path="/About" element={<About />} />
        </Routes>
      </div>
    </Router>
  );
};

export default App;
