import React, { useState } from 'react';
import Header from '../components/Header/Header';
import Body from '../components/Body/Body';
import Footer from '../components/Footer/Footer';

const Home = () => {
  const [searchTerm, setSearchTerm] = useState('');

  return (
    <div>
      <Header onSearchChange={setSearchTerm} />
      <Body searchTerm={searchTerm} />
      <Footer />
    </div>
  );
};

export default Home;
