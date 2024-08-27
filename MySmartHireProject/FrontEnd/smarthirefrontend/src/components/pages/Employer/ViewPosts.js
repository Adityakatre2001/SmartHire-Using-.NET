import React, { useEffect, useState } from 'react';
import EmployerService from '../../../services/EmployerService';
import '../Employer/css/Employer.css';
import { Link } from 'react-router-dom';

export default function ViewPosts() {
    const [jobPostings, setJobPostings] = useState([]);

    useEffect(() => {
        EmployerService.getAllJobPostings()
            .then(response => {
                setJobPostings(response.data);
            })
            .catch(error => {
                console.error('There was an error fetching the job postings!', error);
            });
    }, []);

    return (
        <div className="employer-container">
            <h2>Job Postings</h2>
            <ul>
                {jobPostings.map(job => (
                    <li key={job.jobId}>
                        <h3>{job.jobTitle}</h3>
                        <p>{job.jobDescription}</p>
                        <p>Location: {job.jobLocation}</p>
                        <p>Salary: {job.salary}</p>
                        <p>Posted on: {job.postDate}</p>
                        <p>Closing on: {job.closeDate}</p>
                        <Link to={`/employers/jobs/edit/${job.jobId}`}>Edit</Link> | <Link to={`/employers/jobs/${job.jobId}/applications`}>View Applications</Link>
                    </li>
                ))}
            </ul>
        </div>
    );
}
