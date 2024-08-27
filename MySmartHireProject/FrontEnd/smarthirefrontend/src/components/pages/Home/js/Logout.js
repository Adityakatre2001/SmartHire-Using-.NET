import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import '../css/Logout.css'; // Ensure you have a CSS file for Logout if needed

export default function Logout() {
  const navigate = useNavigate();
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    // Clear user-related data from local storage
    localStorage.removeItem('userId');
    localStorage.removeItem('bookingId'); 
    localStorage.removeItem('tourData');
    localStorage.removeItem('tourId');

    // Simulate a loading delay if needed (for UX purposes)
    setTimeout(() => {
      setLoading(false);
      navigate('/signin'); // Redirect to sign-in page after clearing data
    }, 1000); // Adjust time as needed
  }, [navigate]);

  return (
    <div className="logout-container">
      {loading ? (
        <div className="text-center">
          <h2>Logging out...</h2>
          {/* Optional: Add a spinner or loading indicator */}
          <div className="spinner"></div>
        </div>
      ) : (
        <div className="text-center">
          <h2>Successfully logged out</h2>
          <p>Redirecting to the sign-in page...</p>
        </div>
      )}
    </div>
  );
}
