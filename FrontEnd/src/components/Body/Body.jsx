import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import './Body.scss';

const Body = ({ searchTerm }) => {
  const [products, setProducts] = useState([]);
  const [priceFilter, setPriceFilter] = useState({ min: '', max: '' });
  const [selectedCategory, setSelectedCategory] = useState('');
  useEffect(() => {
    fetchProducts();
  }, []);
  const fetchProducts = () => {
    fetch('https://localhost:44397/api/product/products')
      .then(response => response.json())
      .then(data => {
        setProducts(data);
      })
      .catch(error => {
        console.error('Fel vid hämtning av produkter:', error);
      });
  };
  const uniqueCategories = [...new Set(products.map(product => product.category).filter(Boolean))];
  const filteredProducts = products.filter(product => {
    const matchesSearch = product.productName.toLowerCase().includes(searchTerm.toLowerCase()) || 
                          (product.category && product.category.toLowerCase().includes(searchTerm.toLowerCase()));
    const withinPriceRange =
      (priceFilter.min === '' || product.price >= Number(priceFilter.min)) &&
      (priceFilter.max === '' || product.price <= Number(priceFilter.max));
    const matchesCategory = selectedCategory ? product.category === selectedCategory : true;
    return matchesSearch && withinPriceRange && matchesCategory;
  });
  return (
    <div className="Body">
      <div className="filter-controls">
        <div className="price-filter">
          <input
            type="number"
            placeholder="Min pris"
            value={priceFilter.min}
            onChange={e => setPriceFilter({ ...priceFilter, min: e.target.value })}
            className="price-input"
          />
          <input
            type="number"
            placeholder="Max pris"
            value={priceFilter.max}
            onChange={e => setPriceFilter({ ...priceFilter, max: e.target.value })}
            className="price-input"
          />
        </div>
        <div className="category-filter">
          <button
            onClick={() => setSelectedCategory('')}
            className={`category-button ${selectedCategory === '' ? 'active' : ''}`}
          >
            Alla produkter
          </button>
          {uniqueCategories.map(category => (
            <button
              key={category}
              onClick={() => setSelectedCategory(category)}
              className={`category-button ${selectedCategory === category ? 'active' : ''}`}
            >
              {category}
            </button>
          ))}
        </div>
      </div>
      <div className="searchResults">
        {searchTerm && (
          filteredProducts.length > 0 ? (
            <p>{filteredProducts.length} träffar hittades för "{searchTerm}".</p>
          ) : (
            <p>Inga träffar hittades för "{searchTerm}".</p>
          )
        )}
      </div>
      <div className="product-list">
        {filteredProducts.length > 0 ? (
          filteredProducts.map(product => (
            <Link to={`/product/${product.id}`} key={product.id} className="product-link">
              <div className="product-card">
                <h3>{product.productName}</h3>
                <img src={product.imageUrl} alt={product.productName} className="product-image" />
                <p>Kategori: {product.category || 'Ingen kategori'}</p>
                <p className="product-price">Pris: {product.price} SEK</p>
                <p dangerouslySetInnerHTML={{ __html: product.cardDescription }}></p>
              </div>
            </Link>
          ))
        ) : (
          <p>Inga produkter att visa.</p>
        )}
      </div>
    </div>
  );
};
export default Body;
