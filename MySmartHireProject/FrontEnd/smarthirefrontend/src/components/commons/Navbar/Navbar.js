

import React from 'react';
import { Link } from 'react-router-dom';
import '../Navbar/Navbar.css'

//import smarthire from './logos/smarthire.png';

function Navbar() {
  return (
    <nav className="navbar">
      <div className="navbar-logo">
        {/* Clicking on SmartHire will reload the page */}
        <Link to="/" className="navbar-logo-link">
        {/* <img src={smarthire} alt="SmartHire Logo" className="navbar-logo-img" /> */}
        <link rel="apple-touch-icon" href="%PUBLIC_URL%/smarthire.png" /><h1>SmartHire</h1>
        </Link>
      </div>
      <ul className="navbar-links">
        
         <li><Link to="/admin">Admin  </Link></li>
         <li><Link to="/employer">Recruiter</Link></li>
         <li><Link to="/about" >About</Link></li>
        <li><Link to="/contact">Contact</Link></li>
        <li><Link to ="/signin">Sign In</Link></li> {/* Added Sign In link */}
        <li><Link to ="/register">Register</Link></li> 
        <li><Link to ="/logout">Logout</Link></li>{/* Added Register link */}
      </ul>
    </nav>
  );
}

export default Navbar;

