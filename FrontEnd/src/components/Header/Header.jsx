import React, { useState } from 'react';
import { Link } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faUser } from '@fortawesome/free-solid-svg-icons';
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
      
      <span onClick={openModal} className="LogSignIn">
        <FontAwesomeIcon icon={faUser} size="lg" /> {/* Login Icon */}
      </span>
      {isModalOpen && <AuthModal onClose={closeModal} />}
    </div>
  );
};

export default Header;


