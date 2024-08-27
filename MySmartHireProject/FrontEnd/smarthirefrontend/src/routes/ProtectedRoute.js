import React from 'react';
import { Navigate } from 'react-router-dom';

function ProtectedRoute({ children }) {
    const role = localStorage.getItem('role');

    return role === 'admin' ? children : <Navigate to="/signin" />;
}

export default ProtectedRoute;
