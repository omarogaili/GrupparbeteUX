import React, { useState } from 'react';
import Header from '../components/Header/Header';
import Footer from '../components/Footer/Footer';
import './AddItem.scss';

const AddItem = () => {
  const [product, setProduct] = useState({
    name: '',
    description: '',
    price: '',
    image: '',
  });

  const handleChange = (e) => {
    const { name, value } = e.target;
    setProduct((prevProduct) => ({
      ...prevProduct,
      [name]: value,
    }));
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    fetch("https://localhost:44397/api/product", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(product),
    })
      .then((response) => response.json())
      .then((data) => {
        console.log("Product added:", data);
      })
      .catch((error) => console.error("Error adding product:", error));
  };

  return (
    <div>
      <Header />
      <div className="add-item-container">
        <form onSubmit={handleSubmit} className="add-item-form">
          <label>
            Name:
            <input type="text" name="name" value={product.name} onChange={handleChange} />
          </label>
          <label>
            Description:
            <input type="text" name="description" value={product.description} onChange={handleChange} />
          </label>
          <label>
            Price:
            <input type="number" name="price" value={product.price} onChange={handleChange} />
          </label>
          <label>
            Image URL:
            <input type="text" name="image" value={product.image} onChange={handleChange} />
          </label>
          <button type="submit">Add Product</button>
        </form>
      </div>
      <Footer />
    </div>
  );
};

export default AddItem;
