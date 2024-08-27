import React, { useState, useEffect } from 'react';
import AdminService from '../../../../services/AdminService';

function AllUsers() {
    const [users, setUsers] = useState([]);

    useEffect(() => {
        AdminService.getAllUsers().then(response => {
            setUsers(response.data);
        });
    }, []);

    return (
        <div>
            <h2>All Users</h2>
            <ul>
                {users.map(user => (
                    <li key={user.id}>
                        {user.name} - <a href={`/users/${user.id}`}>Details</a>
                    </li>
                ))}
            </ul>
        </div>
    );
}

export default AllUsers;
