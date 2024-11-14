import { useState, useEffect } from 'react';
import { useNavigate } from "react-router-dom";
import Dashboard from '../../Pages/Dashboard/Dashboard';
import './AuthModal.scss';

const AuthModal = ({ onClose }) => {
  const navigate = useNavigate();
  const [activeForm, setActiveForm] = useState('login');
  const [formData, setFormData] = useState({
    username: '',
    email: '',
    password: '',
  });
  const [isLoggedIn, setIsLoggedIn] = useState(false);

  useEffect(() => {
    if (localStorage.getItem('token')) {
      setIsLoggedIn(true);
    }
  }, []);

  const toggleForm = (form) => {
    setActiveForm(form);
    setFormData({ username: '', email: '', password: '' }); 
  };

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData((prev) => ({
      ...prev,
      [name]: value,
    }));
  };

  const handleLogin = async (e) => {
    e.preventDefault();
    try {
      const response = await fetch('https://localhost:7091/api/login', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({
          userEmail: formData.email,  
          userPassword: formData.password,
        }),
        credentials: 'include',
      });
      
      if (response.ok) {
        const responseData = await response.json();
        alert('Inloggning lyckades: ' + JSON.stringify(responseData));
        localStorage.setItem('token', responseData.token);
        localStorage.setItem('userEmail', responseData.userEmail);
        setIsLoggedIn(true);
        console.log('Inloggad som:', responseData.userEmail);
        navigate('/dashboard');
      } else {
        const errorData = await response.json();
        console.error('Error logging in:', errorData);
        alert(errorData.message || 'Inloggning misslyckades');
      }
    } catch (error) {
      console.error('Error logging in:', error);
      alert('Ett fel inträffade vid inloggning.');
    }
  };

  const handleSignup = async (e) => {
    e.preventDefault();
    try {
      const response = await fetch('https://localhost:7091/api/signup', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({
          userName: formData.username,  
          userEmail: formData.email,     
          userPassword: formData.password
        }),
      });
      const data = await response.json();
      if (response.ok) {
        alert('Registrering lyckades');
      } else {
        alert(data.message || 'Registrering misslyckades');
      }
    } catch (error) {
      console.error('Error signing up:', error);
      alert('Ett fel inträffade vid registrering.');
    }
  };

  const handleLogout = async () => {
    try {
      const response = await fetch('https://localhost:7091/api/logout', {
        method: 'POST',
        credentials: 'include',
      });

      if (response.ok) {
        localStorage.removeItem('token');
        setIsLoggedIn(false);
        navigate('/'); 
        alert('Utloggning lyckades');
      } else {
        alert('Utloggning misslyckades');
      }
    } catch (error) {
      console.error('Error logging out:', error);
    }
  };

  return (
    <div className="auth-modal-overlay">
      <div className="auth-modal">
        <div className="form-toggle">
          {!isLoggedIn ? (
            <>
              <button onClick={() => toggleForm('login')} className={activeForm === 'login' ? 'active' : ''}>
                Logga in
              </button>
              <button onClick={() => toggleForm('signup')} className={activeForm === 'signup' ? 'active' : ''}>
                Registrera
              </button>
            </>
          ) : (
            <button onClick={handleLogout}>Logga ut</button>
          )}
        </div>

        <div className="form-container">
          {isLoggedIn ? (
            <div>
              <Dashboard showHeader={false} showFooter={false} />
            </div>
          ) : (
            <>
              {activeForm === 'login' && (
                <div className="form active">
                  <h2>Logga in</h2>
                  <form onSubmit={handleLogin}>
                    <input
                      type="email"
                      name="email"
                      placeholder="E-post"
                      value={formData.email}
                      onChange={handleChange}
                    />
                    <input
                      type="password"
                      name="password"
                      placeholder="Lösenord"
                      value={formData.password}
                      onChange={handleChange}
                    />
                    <button type="submit">Logga in</button>
                  </form>
                </div>
              )}
              {activeForm === 'signup' && (
                <div className="form active">
                  <h2>Registrera dig</h2>
                  <form onSubmit={handleSignup}>
                    <input
                      type="text"
                      name="username"
                      placeholder="Användarnamn"
                      value={formData.username}
                      onChange={handleChange}
                    />
                    <input
                      type="email"
                      name="email"
                      placeholder="E-post"
                      value={formData.email}
                      onChange={handleChange}
                    />
                    <input
                      type="password"
                      name="password"
                      placeholder="Lösenord"
                      value={formData.password}
                      onChange={handleChange}
                    />
                    <button type="submit">Registrera</button>
                  </form>
                </div>
              )}
            </>
          )}
        </div>
        <button onClick={onClose} className="close-button">Stäng</button>
      </div>
    </div>
  );
};

export default AuthModal;
