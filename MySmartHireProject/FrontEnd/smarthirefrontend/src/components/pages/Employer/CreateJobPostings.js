import React, { useState } from 'react';
import EmployerService from '../../../services/EmployerService';
import '../Employer/css/Employer.css';

export default function CreateJobPosting() {
    const [jobPost, setJobPost] = useState({
        jobTitle: '',
        jobDescription: '',
        salary: '',
        jobLocation: '',
        postDate: '',
        closeDate: '',
        employerId: '' // or fetch this from logged-in employer data
    });

    const handleChange = (e) => {
        setJobPost({
            ...jobPost,
            [e.target.name]: e.target.value
        });
    };

    const handleSubmit = (e) => {
        e.preventDefault();
        EmployerService.createJobPosting(jobPost)
            .then(response => {
                console.log('Job posted successfully:', response.data);
                // Redirect or update the UI accordingly
            })
            .catch(error => {
                console.error('There was an error creating the job posting!', error);
            });
    };

    return (
        <div className="employer-container">
            <h2>Create Job Posting</h2>
            <form onSubmit={handleSubmit}>
                <input type="text" name="jobTitle" placeholder="Job Title" value={jobPost.jobTitle} onChange={handleChange} />
                <textarea name="jobDescription" placeholder="Job Description" value={jobPost.jobDescription} onChange={handleChange}></textarea>
                <input type="number" name="salary" placeholder="Salary" value={jobPost.salary} onChange={handleChange} />
                <input type="text" name="jobLocation" placeholder="Job Location" value={jobPost.jobLocation} onChange={handleChange} />
                <input type="date" name="postDate" value={jobPost.postDate} onChange={handleChange} />
                <input type="date" name="closeDate" value={jobPost.closeDate} onChange={handleChange} />
                <button type="submit">Create Job Posting</button>
            </form>
        </div>
    );
}
