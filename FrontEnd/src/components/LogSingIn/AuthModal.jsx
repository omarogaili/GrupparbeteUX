import React, { useState } from 'react';
import './AuthModal.scss';

const AuthModal = ({ onClose }) => {
  const [activeForm, setActiveForm] = useState('login'); 

  const toggleForm = (form) => {
    setActiveForm(form); 
  };

  return (
    <div className="auth-modal-overlay">
      <div className="auth-modal">
        <div className="form-toggle">
          <button onClick={() => toggleForm('login')} className={activeForm === 'login' ? 'active' : ''}>
            Logga in
          </button>
          <button onClick={() => toggleForm('signup')} className={activeForm === 'signup' ? 'active' : ''}>
            Registrera
          </button>
        </div>

        <div className="form-container">

          <div className={`form ${activeForm === 'login' ? 'active' : ''}`}>
            <h2>Logga in</h2>
            <form>
              <input type="text" placeholder="Användarnamn" />
              <input type="password" placeholder="Lösenord" />
              <button type="submit">Logga in</button>
            </form>
          </div>

          <div className={`form ${activeForm === 'signup' ? 'active' : ''}`}>
            <h2>Registrera dig</h2>
            <form>
              <input type="text" placeholder="Användarnamn" />
              <input type="email" placeholder="E-post" />
              <input type="password" placeholder="Lösenord" />
              <button type="submit">Registrera</button>
            </form>
          </div>
        </div>
        <button onClick={onClose} className="close-button">Stäng</button>
      </div>
    </div>
  );
};

export default AuthModal;
