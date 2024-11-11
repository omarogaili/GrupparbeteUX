import React from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faFacebook, faTwitter, faTiktok } from '@fortawesome/free-brands-svg-icons';
import './Footer.scss'; 

const Footer = () => {
  return (
    <div className="Footer">
      <div className="div1">
        <h4>Om Oss</h4>
      </div>
      <div className="div2">
        <h4>Kontakt</h4>
      </div>
      <div className="div3">
        <h4>Följ Oss</h4>
        <div className="socialMediaIcons">
          <FontAwesomeIcon icon={faFacebook} className="icon" />
          <FontAwesomeIcon icon={faTwitter} className="icon" />
          <FontAwesomeIcon icon={faTiktok} className="icon" />
        </div>
      </div>
    </div>
  );
};

export default Footer;
