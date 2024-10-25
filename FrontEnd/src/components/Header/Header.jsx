import React from 'react';
import './Header.scss'; 

const Header = () => {
  return (
    <div className="Header">
      <div className="LogaNamn">Logga/Namn</div>
      <div className="Kategorier">Kategorier</div>
      <div className="LagInAnnons">Lägg in annons</div>
      <div className="Search">Sök</div>
      <div className="LogSignIn">Logga in / Registrera</div>
    </div>
  );
};

export default Header;
