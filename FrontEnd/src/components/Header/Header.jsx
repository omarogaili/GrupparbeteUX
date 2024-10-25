import React from 'react';
import { Link } from 'react-router-dom';
import './Header.scss';

const Header = () => {
  return (
    <div className="Header">
      <Link to="/" className="LogaNamn">Logga/Namn</Link>
      <Link to="/view-item" className="Kategorier">Kategorier</Link>
      <Link to="/mina-annonser/lagg-in-annons" className="LagInAnnons">Lägg in annons</Link>
      <Link to="/annonser" className="Search">Annonser</Link>
      <Link to="/login" className="LogSignIn">Logga in / Registrera</Link>
    </div>
  );
};

export default Header;
