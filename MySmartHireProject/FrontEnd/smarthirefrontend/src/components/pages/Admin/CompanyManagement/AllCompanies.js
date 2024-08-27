import React, { useState, useEffect } from 'react';
import AdminService from '../../../../services/AdminService';

function AllCompanies() {
    const [companies, setCompanies] = useState([]);

    useEffect(() => {
        AdminService.getAllCompanies().then(response => {
            setCompanies(response.data);
        });
    }, []);

    return (
        <div>
            <h2>All Companies</h2>
            <ul>
                {companies.map(company => (
                    <li key={company.id}>
                        {company.name} - <a href={`/companies/${company.id}`}>Details</a>
                    </li>
                ))}
            </ul>
        </div>
    );
}

export default AllCompanies;
