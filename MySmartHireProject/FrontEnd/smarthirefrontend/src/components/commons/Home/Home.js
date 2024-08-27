import React from 'react';
import '../Home/Home.css'; // Import the CSS file for styling
import smarthirehomepage from '../assets/smarthirehomepage.jpg';

function Home() {
  return (
    <div className="home-container">
      
      <div className="home-content">
        <h1>Welcome to SmartHire</h1>
        {/* <p>Your gateway to finding the best job opportunities.</p> */}
        <p>
          SmartHire is designed to connect job seekers with top employers. Whether you're looking 
          for your first job or aiming to take the next step in your career, SmartHire provides 
          the tools and resources to help you succeed. Explore job listings, create a professional 
          profile, and apply to the best positions with ease.
        </p>
        <p>
          Join our platform today and start your journey towards a fulfilling career. With SmartHire, 
          finding the right job is just a few clicks away. Your dream job is out there—let us help you 
          find it.
        </p>
      </div>
      <div className="home-image">
        <img src={smarthirehomepage} alt="SmartHire" />
      </div>
    </div>
  );
}

export default Home;
