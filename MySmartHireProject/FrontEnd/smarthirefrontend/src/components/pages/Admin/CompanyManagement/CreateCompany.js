import React, { useState } from 'react';
import AdminService from '../../../../services/AdminService';

function CreateCompany() {
    const [company, setCompany] = useState({ name: '', location: '', industry: '' });

    const handleChange = (e) => {
        setCompany({ ...company, [e.target.name]: e.target.value });
    };

    const handleSubmit = (e) => {
        e.preventDefault();
        AdminService.createCompany(company).then(() => window.location.href = '/companies');
    };

    return (
        <div>
            <h2>Create Company</h2>
            <form onSubmit={handleSubmit}>
                <input name="name" value={company.name} onChange={handleChange} placeholder="Name" required />
                <input name="location" value={company.location} onChange={handleChange} placeholder="Location" required />
                <input name="industry" value={company.industry} onChange={handleChange} placeholder="Industry" required />
                <button type="submit">Create Company</button>
            </form>
        </div>
    );
}

export default CreateCompany;
