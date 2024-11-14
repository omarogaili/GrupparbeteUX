import   { useState, useEffect } from 'react';
import Footer from '../Footer/Footer';
import Header from '../Header/Header';
import './cart.scss';
const Cart = () => {
    const [cartItems, setCartItems] = useState([]);
    const [totalPrice, setTotalPrice] = useState(0);
    const [purchaseStatus, setPurchaseStatus] = useState('');
    useEffect(() => {
        const cart = JSON.parse(localStorage.getItem('cart')) || [];
        setCartItems(cart);
        const total = cart.reduce((sum, item) => sum + item.price, 0);
        setTotalPrice(total);
    }, []);
    const handlePurchase = async () => {
        try {
            const response = await fetch('https://localhost:7091/api/Bill', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({
                    items: cartItems,
                    totalAmount: totalPrice
                })
            });
            if (!response.ok) {
                throw new Error('Fel vid utförande av köpet');
            }
            setPurchaseStatus('Köpet har genomförts!');
            localStorage.removeItem('cart');
            setCartItems([]);
            setTotalPrice(0);
        } catch (error) {
            setPurchaseStatus('Köpet misslyckades. Försök igen.');
            console.error(error);
        }
    };
    const handleRemovePurchase = async () => {
        
            setPurchaseStatus('Köpet har blivit borttagen!');
            localStorage.removeItem('cart');
            setCartItems([]);
            setTotalPrice(0);
    };
    return (
        <div className='Container'>
            <Header />
            <h1>Kundvagn</h1>
            {cartItems.length === 0 ? (
                <p>Kundvagnen är tom.</p>
            ) : (
                <div className='cartContainer'>
                    <ul>
                        {cartItems.map((item, index) => (
                            <li key={index}>
                                <b>{item.productName}</b> - {item.price} SEK
                            </li>
                        ))}
                    </ul>
                    <p>Totalt pris: {totalPrice} SEK</p>
                    <button onClick={handlePurchase}>Utför köp</button>
                    <button className='removeItem' onClick={handleRemovePurchase}>Ta bort köp</button>
                    {purchaseStatus && <p>{purchaseStatus}</p>}
                </div>
            )}
            <Footer />
        </div>
    );
};

export default Cart;
