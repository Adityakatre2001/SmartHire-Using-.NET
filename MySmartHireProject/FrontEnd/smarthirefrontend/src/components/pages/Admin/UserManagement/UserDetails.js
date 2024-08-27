import React, { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';
import AdminService from '../../../../services/AdminService';

function UserDetail() {
    const { id } = useParams();
    const [user, setUser] = useState(null);

    useEffect(() => {
        AdminService.getUserById(id).then(response => {
            setUser(response.data);
        });
    }, [id]);

    return (
        <div>
            {user ? (
                <div>
                    <h2>{user.name}</h2>
                    <p>Email: {user.email}</p>
                    <p>Role: {user.role}</p>
                    <button onClick={() => AdminService.deleteUser(id).then(() => window.location.reload())}>
                        Delete User
                    </button>
                    <button onClick={() => window.location.href = `/users/update/${id}`}>Update User</button>
                </div>
            ) : (
                <p>Loading...</p>
            )}
        </div>
    );
}

export default UserDetail;
