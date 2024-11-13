import { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { Link } from 'react-router-dom';
import CheckBox from'./CheckBox';
import './styles.scss'

const ProductDetail = () => {
    const { id } = useParams();
    const navigate = useNavigate();
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
    const addToCart = () => {
        const cart = JSON.parse(localStorage.getItem('cart')) || [];
        cart.push(product);
        localStorage.setItem('cart', JSON.stringify(cart));
        alert(`${product.productName} har lagts till i korgen!`);
    };
    if (loading) return <p>Laddar...</p>;
    if (error) return <p>{error}</p>;
    return (
        <div>
            {product ? (
                <div className='MainContainer'>
                    <div className='CardContainer'>
                        <h1>{product.productName}</h1>
                        <img src={product.imageUrl} alt={product.productName} />
                        <p>Kategori: {product.category}</p>
                        <p>Pris: {product.price} SEK</p>
                        <div className='CardLinks'>
                            <button onClick={addToCart}>Lägg till i korgen</button>
                            <button onClick={() => navigate(-1)}>Gå tillbaka</button>
                            <Link to="/cart">Gå till kundvagnen</Link>
                        </div>
                    </div>
                    <p>Beskrivning: {product.description}</p>
                    <CheckBox />
                </div>
            ) : (
                <p>Produkten kunde inte hittas.</p>
            )}
        </div>
    );
};
export default ProductDetail;
