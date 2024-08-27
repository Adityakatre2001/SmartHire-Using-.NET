import React, { useState } from 'react';
import AdminService from '../../../../services/AdminService';

function CreateUser() {
    const [user, setUser] = useState({ name: '', email: '', role: '' });

    const handleChange = (e) => {
        setUser({ ...user, [e.target.name]: e.target.value });
    };

    const handleSubmit = (e) => {
        e.preventDefault();
        AdminService.createUser(user).then(() => window.location.href = '/users');
    };

    return (
        <div>
            <h2>Create User</h2>
            <form onSubmit={handleSubmit}>
                <input name="name" value={user.name} onChange={handleChange} placeholder="Name" required />
                <input name="email" value={user.email} onChange={handleChange} placeholder="Email" required />
                <input name="role" value={user.role} onChange={handleChange} placeholder="Role" required />
                <button type="submit">Create User</button>
            </form>
        </div>
    );
}

export default CreateUser;
