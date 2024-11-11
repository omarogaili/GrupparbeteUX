import React, { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';
import './styles.scss'
const ProductDetail = () => {
    const { id } = useParams();
    const [product, setProduct] = useState(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        const fetchProductDetail = async () => {
            try {
                const response = await fetch(`https://localhost:44397/api/product/product/${id}`);
                if (!response.ok) {
                    throw new Error('Fel vid hämtning av produkten');
                }
                const data = await response.json();
                setProduct(data);
            } catch (error) {
                setError(error.message);
            } finally {
                setLoading(false);
            }
        };

        fetchProductDetail();
    }, [id]);

    if (loading) return <p>Laddar...</p>;
    if (error) return <p>{error}</p>;

    return (
        <div>
            {product ? (
                <div>
                    <h1>{product.productName}</h1>
                    <img src={product.imageUrl} alt={product.productName} />
                    <p>Kategori: {product.category}</p>
                    <p>Beskrivning: {product.description}</p>
                    <p>Pris: {product.price} SEK</p>
                </div>
            ) : (
                <p>Produkten kunde inte hittas.</p>
            )}
        </div>
    );
};

export default ProductDetail;
