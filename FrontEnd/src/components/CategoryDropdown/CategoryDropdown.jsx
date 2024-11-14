import React, { useState, useEffect } from 'react';
const CategoryDropdown = ({ onCategoryChange }) => {
  const [categories, setCategories] = useState([]);

  useEffect(() => {
    // Fetch categories here (or pass them as props from a parent component)
    fetch('https://localhost:44397/api/product/categories') // Example API call for categories
      .then(response => response.json())
      .then(data => setCategories(data))
      .catch(error => console.error('Fel vid hämtning av kategorier:', error));
  }, []);

  return (
    <div className="category-dropdown">
      <select onChange={(e) => onCategoryChange(e.target.value)} defaultValue="">
        <option value="">Alla kategorier</option>
        {categories.map(category => (
          <option key={category} value={category}>
            {category}
          </option>
        ))}
      </select>
    </div>
  );
};

export default CategoryDropdown;
