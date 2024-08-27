import React, { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';
import AdminService from '../../../../services/AdminService';

function UpdateCompany() {
    const { id } = useParams();
    const [company, setCompany] = useState({ name: '', location: '', industry: '' });

    useEffect(() => {
        AdminService.getCompanyById(id).then(response => {
            setCompany(response.data);
        });
    }, [id]);

    const handleChange = (e) => {
        setCompany({ ...company, [e.target.name]: e.target.value });
    };

    const handleSubmit = (e) => {
        e.preventDefault();
        AdminService.updateCompany(id, company).then(() => window.location.href = `/companies/${id}`);
    };

    return (
        <div>
            <h2>Update Company</h2>
            <form onSubmit={handleSubmit}>
                <input name="name" value={company.name} onChange={handleChange} placeholder="Name" required />
                <input name="location" value={company.location} onChange={handleChange} placeholder="Location" required />
                <input name="industry" value={company.industry} onChange={handleChange} placeholder="Industry" required />
                <button type="submit">Update Company</button>
            </form>
        </div>
    );
}

export default UpdateCompany;
