import React, { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';
import AdminService from '../../../../services/AdminService';

function CompanyDetails() {
    const { id } = useParams();
    const [company, setCompany] = useState(null);

    useEffect(() => {
        AdminService.getCompanyById(id).then(response => {
            setCompany(response.data);
        });
    }, [id]);

    return (
        <div>
            {company ? (
                <div>
                    <h2>{company.name}</h2>
                    <p>Location: {company.location}</p>
                    <p>Industry: {company.industry}</p>
                    <button onClick={() => AdminService.deleteCompany(id).then(() => window.location.reload())}>
                        Delete Company
                    </button>
                    <button onClick={() => window.location.href = `/companies/update/${id}`}>Update Company</button>
                </div>
            ) : (
                <p>Loading...</p>
            )}
        </div>
    );
}

export default CompanyDetails;
