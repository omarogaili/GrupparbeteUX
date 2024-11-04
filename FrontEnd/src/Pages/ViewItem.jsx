import React, { useEffect, useState } from 'react';
import Header from '../components/Header/Header';
import Footer from '../components/Footer/Footer';
import './ViewItem.scss';

const ViewItem = () => {
  const [products, setProducts] = useState([]);

  useEffect(() => {
    fetch("https://localhost:44397/api/product/products") 
      .then((response) => response.json())
      .then((data) => setProducts(data))
      .catch((error) => console.error("Error fetching products:", error));
  }, []);

  return (
    <div>
      <Header />
      <div className="view-item-container">
        <h2>Products</h2>
        <ul className="product-list">
          {products.map((product) => (
            <li key={product.id} className="product-item">
              <h3 className="product-title">{product.name}</h3>
              <p className="product-description">{product.description}</p>
              <p className="product-price">Price: ${product.price}</p>
            </li>
          ))}
        </ul>
      </div>
      <Footer />
    </div>
  );
};

export default ViewItem;
