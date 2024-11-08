import React, { useEffect, useState } from 'react';
import Header from '../components/Header/Header';
import Footer from '../components/Footer/Footer';
import './ViewItem.scss';

const ViewItem = () => {
  const [products, setProducts] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [category, setCategory] = useState('');

  const categories = ['kläder', 'hem', 'sport', 'skor'];

  const fetchProducts = async (category) => {
    try {
      const url = category
        ? `https://localhost:44397/api/product/category?category=${category}`
        : 'https://localhost:44397/api/product/products';

      const response = await fetch(url);
      if (!response.ok) {
        throw new Error('Network response was not ok ' + response.statusText);
      }
      const data = await response.json();
      setProducts(data);
    } catch (error) {
      console.error("Error fetching products:", error);
      setError(error.message);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchProducts(category);
  }, [category]);

  return (
    <div>
      <Header />
      <div className="view-item-container">
        <h2>Produkter</h2>
        {loading && <p>Laddar...</p>}
        {error && <p>Fel: {error}</p>}
        
        <div className="button-container">
          <button onClick={() => setCategory('')}>Visa Alla Produkter</button>
          {categories.map((cat) => (
            <button key={cat} onClick={() => setCategory(cat)}>
              Visa {cat.charAt(0).toUpperCase() + cat.slice(1)}
            </button>
          ))}
        </div>

        <select
          className="category-select"
          onChange={(e) => setCategory(e.target.value)}
          value={category}
        >
          <option value="">Alla Kategorier</option>
          {categories.map((cat) => (
            <option key={cat} value={cat}>
              {cat.charAt(0).toUpperCase() + cat.slice(1)}
            </option>
          ))}
        </select>

        <ul className="product-list">
          {products.map((product) => (
            <li key={product.id} className="product-item">
              <h3 className="product-title">{product.productName}</h3>
              <p className="product-description">{product.description}</p>
              <p className="product-price">Pris: ${product.price}</p>
              <p className="product-category">Kategori: {product.category}</p>
              {product.imageUrl && <img src={product.imageUrl} alt={product.productName} className="product-image" />}
            </li>
          ))}
        </ul>
      </div>
      <Footer />
    </div>
  );
};

export default ViewItem;
