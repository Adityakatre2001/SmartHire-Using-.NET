import React, { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';
import AdminService from '../../../../services/AdminService';

function UpdateUser() {
    const { id } = useParams();
    const [user, setUser] = useState({ name: '', email: '', role: '' });

    useEffect(() => {
        AdminService.getUserById(id).then(response => {
            setUser(response.data);
        });
    }, [id]);

    const handleChange = (e) => {
        setUser({ ...user, [e.target.name]: e.target.value });
    };

    const handleSubmit = (e) => {
        e.preventDefault();
        AdminService.updateUser(id, user).then(() => window.location.href = `/users/${id}`);
    };

    return (
        <div>
            <h2>Update User</h2>
            <form onSubmit={handleSubmit}>
                <input name="name" value={user.name} onChange={handleChange} placeholder="Name" required />
                <input name="email" value={user.email} onChange={handleChange} placeholder="Email" required />
                <input name="role" value={user.role} onChange={handleChange} placeholder="Role" required />
                <button type="submit">Update User</button>
            </form>
        </div>
    );
}

export default UpdateUser;
