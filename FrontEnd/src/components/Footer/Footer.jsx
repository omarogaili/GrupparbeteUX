import React from 'react';
import './Footer.scss'; 

const Footer = () => {
  return (
    <div className="Footer">
      <div className="div1">
        <h4>Om Oss</h4>
        <p>Information om företaget eller sidan.</p>
      </div>
      <div className="div2">
        <h4>Kontakt</h4>
        <p>Email: info@exempel.se</p>
        <p>Telefon: 123-456789</p>
      </div>
      <div className="div3">
        <h4>Följ Oss</h4>
        <p>Sociala medier länkar eller ikoner.</p>
      </div>
    </div>
  );
};

export default Footer;
