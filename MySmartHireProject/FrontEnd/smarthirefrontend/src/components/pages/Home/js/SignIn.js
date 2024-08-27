import React, { useState } from 'react';
import '../css/SignIn.css';
import { useNavigate } from 'react-router-dom';
import UserService from '../../../../services/UserService';

export default function SignInForm() {
    const [formDetails, setFormDetails] = useState({
        email: '',
        password: ''
    });
    const [errorMessage, setErrorMessage] = useState('');  // New state for error messages

    const navigate = useNavigate();

    const handleChange = (event) => {
        const name = event.target.name;
        setFormDetails({
            ...formDetails,
            [name]: event.target.value
        });
    };

    const signInUser = (event) => {
        event.preventDefault(); // Prevent the default form submission

        if (formDetails.email === '' || formDetails.password === '') {
            alert('Please fill all the details');
            return;
        }

        UserService.userSignIn(formDetails)
            .then((result) => {
                console.log('User signed in successfully:', result);

                const user = result.data;
                if (user && user.userId) {
                    localStorage.setItem('userId', user.userId);
                    console.log('User role:', user.role);
                    if (user.role === 'ROLE_APPLICANT') {
                        navigate('/jobpostings');
                    } else if (user.role === 'ROLE_ADMIN') {
                        navigate('/admin/dashboard');
                    } else if (user.role === 'ROLE_EMPLOYER') {
                        navigate('/employer/dashboard');
                    }
                } else {
                    alert('Failed to retrieve user information.');
                }
            })
            .catch((error) => {
                console.error('There was an error signing in!', error);
                // Set the error message based on the response
                setErrorMessage(error.response?.data?.message || 'Failed to sign in. Please check your credentials and try again.');
            });
    };

    const handleForgotPassword = () => {
        navigate('/reset-password');
    };

    return (
        <div className="signin-container">
            <div className="signin-form">
                <h2>Sign In</h2>
                <form onSubmit={signInUser}>
                    <div className="form-group">
                        <label htmlFor="email">Email</label>
                        <input 
                            type="email"
                            id="email"
                            name="email"
                            className="form-control"
                            placeholder="Enter email"
                            value={formDetails.email}
                            onChange={handleChange}
                            required 
                        />
                    </div>
                    <div className="form-group">
                        <label htmlFor="password">Password</label>
                        <input 
                            type="password"
                            id="password"
                            name="password"
                            className="form-control"
                            placeholder="Enter password"
                            value={formDetails.password}
                            onChange={handleChange}
                            required 
                        />
                    </div>
                    <button type="submit" className='submit-button'>
                        Sign In
                    </button>
                </form>
                {errorMessage && <p className="error-message">{errorMessage}</p>}
            </div>
        </div>
    );
}
