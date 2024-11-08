import React, { useState } from 'react';
import { Link } from 'react-router-dom';
import './Header.scss';
import AuthModal from '../LogSingIn/AuthModal';
import CategoryDropdown from '../CategoryDropdown/CategoryDropdown';

const Header = () => {
  const [isModalOpen, setIsModalOpen] = useState(false);

  const openModal = () => setIsModalOpen(true);
  const closeModal = () => setIsModalOpen(false);

  return (
    <div className="Header">
      <Link to="/" className="LogaNamn">Logga/Namn</Link>
      <div className="Kategorier">
        <CategoryDropdown /> {/* Placera dropdown här */}
      </div>
      <Link to="/mina-annonser/lagg-in-annons" className="LagInAnnons">Lägg in annons</Link>
      <Link to="/annonser" className="Search">Annonser</Link>
      <span onClick={openModal} className="LogSignIn">Logga in / Registrera</span>
      {isModalOpen && <AuthModal onClose={closeModal} />}
    </div>
  );
};

export default Header;
