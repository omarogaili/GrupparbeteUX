import{ useState } from 'react';
import { useNavigate } from "react-router-dom";

export default function SignOut() {
  const navigate = useNavigate();
    const [loggedIn, setLoggedIn] = useState(false);
    const handleLogout = async () => {
        try {
            const response = await fetch('https://localhost:7091/api/logout', { // Endpoint för utloggning
                method: 'POST',
                credentials: 'include', // Skicka med cookies vid utloggning
            });
            if (response.ok) {
                localStorage.removeItem('token'); // Ta bort token vid utloggning
                setLoggedIn(false); // Uppdatera tillståndet
                navigate('/'); // Navigera tillbaka till startsidan
                alert('Utloggning lyckades');
            } else {
                alert('Utloggning misslyckades');
            }
        } catch (error) {
            console.error('Error logging out:', error);
        }
    };
    return (
            <div>
                <button onClick={handleLogout}>Logga ut</button>
            </div>
    );
}