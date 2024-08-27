import React, { useEffect, useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import AdminService from '../../../services/AdminService';
import '../Admin/css/AdminDashboard.css';

export default function Dashboard() {
  const [users, setUsers] = useState([]);
  const [companies, setCompanies] = useState([]);
  const navigate = useNavigate();

  useEffect(() => {
    AdminService.listAllUsers().then((response) => {
      setUsers(response.data);
    });

    AdminService.getAllCompanies().then((response) => {
      setCompanies(response.data);
    });
  }, []);

  const updateUser = (userId) => {
    navigate(`/admin/users/edit/${userId}`);
  };

  const deleteUser = (userId) => {
    AdminService.deleteUser(userId).then(() => {
      setUsers(users.filter(user => user.id !== userId));
    });
  };

  const editCompany = (companyId) => {
    navigate(`/admin/companies/edit/${companyId}`);
  };

  const deleteCompany = (companyId) => {
    AdminService.deleteCompany(companyId).then(() => {
      setCompanies(companies.filter(company => company.id !== companyId));
    });
  };

  return (
    <div className="admin-dashboard">
      <h2>Admin Dashboard</h2>
      <div className="table-container">
        <h3>Users</h3>
        <Link to="/admin/users/create" className="add-button">Add User</Link>
        <table className="table">
          <thead>
            <tr>
              <th>ID</th>
              <th>Name</th>
              <th>Role</th>
              <th>Email</th>
              <th>Actions</th>
            </tr>
          </thead>
          <tbody>
            {users.map(user => (
              <tr key={user.userId}>
                <td>{user.userId}</td>
                <td>{user.username}</td>
                <td>{user.role}</td>
                <td>{user.email}</td>
                <td className="actions">
                  <button className="edit-button" onClick={() => updateUser(user.userId)}>Edit</button>
                  <button className="delete-button" onClick={() => deleteUser(user.userId)}>Delete</button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>

      <div className="table-container">
        <h3>Companies</h3>
        <Link to="/admin/companies/create" className="add-button">Add Company</Link>
        <table className="table">
          <thead>
            <tr>
              <th>ID</th>
              <th>Name</th>
              <th>Industry</th>
              <th>Location</th>
              <th>Actions</th>
            </tr>
          </thead>
          <tbody>
            {companies.map(company => (
              <tr key={company.companyId}>
                <td>{company.companyId}</td>
                <td>{company.companyName}</td>
                <td>{company.industry}</td>
                <td>{company.location}</td>
                <td className="actions">
                  <button className="edit-button" onClick={() => editCompany(company.companyId)}>Edit</button>
                  <button className="delete-button" onClick={() => deleteCompany(company.companyId)}>Delete</button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
}
