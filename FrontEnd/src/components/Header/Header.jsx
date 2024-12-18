import { useState } from 'react';
import { Link } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faUser, faBars, faSearch } from '@fortawesome/free-solid-svg-icons';
import './Header.scss';
import AuthModal from '../LogSingIn/AuthModal';
import { faCartShopping } from '@fortawesome/free-solid-svg-icons';
import Logo from '../../assets/Elmarcado.png'

const Header = ({ onSearchChange }) => {
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [isMenuOpen, setIsMenuOpen] = useState(false);
  const [searchTerm, setSearchTerm] = useState('');
  const [searchOpen, setSearchOpen] = useState(false);
  const openModal = () => setIsModalOpen(true);
  const closeModal = () => setIsModalOpen(false);
  const toggleMenu = () => setIsMenuOpen(!isMenuOpen);
  const toggleSearch = () => setSearchOpen(!searchOpen);
  const handleSearchChange = (e) => {
    const newSearchTerm = e.target.value;
    setSearchTerm(newSearchTerm);
    onSearchChange(newSearchTerm);
  };
  return (
    <div className="Header">
      <Link to="/" className="LogaNamn">
        <img src={Logo} alt="Logo" />
      </Link>
      <Link to="/mina-annonser/lagg-in-annons" className="LagInAnnons">Lägg in annons</Link>
      <Link to="/cart">
        <FontAwesomeIcon icon={faCartShopping} className='cartIcon' />
      </Link>
      {/* Search Bar */}
      <div className="Search">
        {searchOpen && (
          <input
            type="text"
            placeholder="Sök produktnamn..."
            value={searchTerm}
            onChange={handleSearchChange}
            className="search-input"
          />
        )}
        <FontAwesomeIcon
          icon={faSearch}
          size="lg"
          className="search-icon"
          onClick={toggleSearch}
        />
      </div>
      <span onClick={openModal} className="LogSignIn">
        <FontAwesomeIcon icon={faUser} size="lg" />
      </span>
      <span onClick={toggleMenu} className="HamburgerMenu">
        <FontAwesomeIcon icon={faBars} size="lg" />
      </span>
      {isModalOpen && <AuthModal onClose={closeModal} />}
      <div className={`MobileMenu ${isMenuOpen ? 'open' : ''}`}>
        <Link to="/" onClick={toggleMenu}>Home</Link>
        <Link to="/kategorier" onClick={toggleMenu}>Kategorier</Link>
        <Link to="/mina-annonser/lagg-in-annons" onClick={toggleMenu}>Lägg in annons</Link>
      </div>
    </div>
  );
};
export default Header;