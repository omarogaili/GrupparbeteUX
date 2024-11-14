import React, { useState } from 'react';
import { Link } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faUser, faBars } from '@fortawesome/free-solid-svg-icons';
import './Header.scss';
import AuthModal from '../LogSingIn/AuthModal';
import CategoryDropdown from '../CategoryDropdown/CategoryDropdown';

const Header = () => {
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [isMenuOpen, setIsMenuOpen] = useState(false);

  const openModal = () => setIsModalOpen(true);
  const closeModal = () => setIsModalOpen(false);
  const toggleMenu = () => setIsMenuOpen(!isMenuOpen);

  return (
    <div className="Header">
      <Link to="/" className="LogaNamn">
        <img src="/path/to/logo.png" alt="Logo" /> {/* Replace with actual logo path */}
      </Link>

      <div className="Kategorier">
        <CategoryDropdown />
      </div>

      <Link to="/mina-annonser/lagg-in-annons" className="LagInAnnons">Lägg in annons</Link>

      <span onClick={openModal} className="LogSignIn">
        <FontAwesomeIcon icon={faUser} size="lg" />
      </span>

      <span onClick={toggleMenu} className="HamburgerMenu">
        <FontAwesomeIcon icon={faBars} size="lg" />
      </span>

      {isModalOpen && <AuthModal onClose={closeModal} />}

      <div className={`MobileMenu ${isMenuOpen ? 'open' : ''}`}>
        <Link to="/" onClick={toggleMenu}>Home</Link>
        <Link to="/kategorier" onClick={toggleMenu}>Kategorier</Link> {/* Add Kategorier link here */}
        <Link to="/mina-annonser/lagg-in-annons" onClick={toggleMenu}>Lägg in annons</Link>
        {/* Add more links as needed */}
      </div>
    </div>
  );
};

export default Header;
