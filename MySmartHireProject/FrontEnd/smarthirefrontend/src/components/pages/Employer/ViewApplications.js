import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import EmployerService from '../../../services/EmployerService';
import '../Employer/css/Employer.css';

export default function ViewApplications() {
    const { jobId } = useParams();
    const [applications, setApplications] = useState([]);

    useEffect(() => {
        EmployerService.getApplicationsForJob(jobId)
            .then(response => {
                setApplications(response.data);
            })
            .catch(error => {
                console.error('There was an error fetching the applications!', error);
            });
    }, [jobId]);

    return (
        <div className="employer-container">
            <h2>Applications</h2>
            <ul>
                {applications.map(applicant => (
                    <li key={applicant.id}>
                        <h3>{applicant.name}</h3>
                        <p>Email: {applicant.email}</p>
                        <p>Resume: <a href={applicant.resumeLink}>Download</a></p>
                    </li>
                ))}
            </ul>
        </div>
    );
}
