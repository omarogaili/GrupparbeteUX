import React, { useState } from 'react';
import { Link } from 'react-router-dom';
import './CategoryDropdown.scss';

const CategoryDropdown = () => {
  const [isDropdownOpen, setIsDropdownOpen] = useState(false);

  // Lista av kategorier med respektive path
  const categories = [
    { name: 'Kläder', path: '/view-item/klader' },
    { name: 'Hem', path: '/view-item/hem' },
    { name: 'Sport', path: '/view-item/sport' },
    { name: 'Skor', path: '/view-item/skor' }
  ];

  const toggleDropdown = () => setIsDropdownOpen((prev) => !prev);

  return (
    <div className="CategoryDropdown">
      <span onClick={toggleDropdown} className="dropdown-toggle">
        Kategorier
      </span>
      {isDropdownOpen && (
        <div className="dropdown-menu">
          {categories.map((category) => (
            <Link
              key={category.name}
              to={category.path}
              className="dropdown-item"
              onClick={() => setIsDropdownOpen(false)}
            >
              {category.name}
            </Link>
          ))}
        </div>
      )}
    </div>
  );
};

export default CategoryDropdown;
