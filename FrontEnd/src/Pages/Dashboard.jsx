import { useEffect, useState } from 'react';
import './Dashboard.scss'

export default function Dashboard() {
    const [userName, setUserName] = useState('');
    const [userId, setUserId] = useState('');
    const [userEmail, setUserEmail] = useState('');
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState('');

    useEffect(() => {
        const token = localStorage.getItem('token');
        
        fetch('https://localhost:7091/api/id', {
            method: 'GET',
            headers: {
                'Authorization': `Bearer ${token}`,
            },
            credentials: 'include',
        })
        .then(response => {
            if (!response.ok) {
                throw new Error('Failed to fetch user data');
            }
            return response.json();
        })
        .then(data => {
            setUserName( data.userName || 'User');
            setUserId( data.userId || 'User');
            setUserEmail( data.userEmail || 'User');
            setLoading(false);
        })
        .catch(error => {
            console.error('Error fetching user data:', error);
            setError('Error fetching user data');
            setLoading(false);
        });
    }, []);
    if (loading) {
        return <div>Loading...</div>;
    }
    return (
        <div className='Container'>
            <h1>Dashboard</h1>
            {error ? (
                <h2>{error}</h2>
            ) : (
                <div className='UserInformation'>
                <h2>Welcome, {userName}</h2>
                <p>User ID: {userId}</p>
                <p>User Email: {userEmail}</p>
                </div>
            )}
        </div>
    );
}
