import React, { useState, useEffect } from 'react';
import axios from 'axios';

const JobPostings = () => {
  const [jobPost, setJobPost] = useState({
    jobId: null,
    postDate: '',
    jobDescription: '',
    employerId: '',
    salary: '',
    jobTitle: '',
    jobLocation: '',
    closeDate: ''
  });

  const [isEditing, setIsEditing] = useState(false);
  
  useEffect(() => {
    // Fetch existing job posting if needed
    const fetchJobPost = async () => {
      try {
        const response = await axios.get('/api/jobposts/1'); // Replace with actual API endpoint
        setJobPost(response.data);
      } catch (error) {
        console.error('Error fetching job posting', error);
      }
    };

    fetchJobPost();
  }, []);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setJobPost({ ...jobPost, [name]: value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      if (isEditing) {
        await axios.put(`/api/jobposts/${jobPost.jobId}`, jobPost);
      } else {
        await axios.post('/api/jobposts', jobPost);
      }
      // Handle success (e.g., redirect or show a success message)
    } catch (error) {
      console.error('Error submitting job posting', error);
    }
  };

  return (
    <div className="job-postings-container">
      <h2>{isEditing ? 'Edit Job Posting' : 'Create Job Posting'}</h2>
      <form onSubmit={handleSubmit} className="job-postings-form">
        <div className="form-group">
          <label htmlFor="postDate">Post Date:</label>
          <input
            type="date"
            id="postDate"
            name="postDate"
            value={jobPost.postDate}
            onChange={handleChange}
            required
          />
        </div>

        <div className="form-group">
          <label htmlFor="jobDescription">Job Description:</label>
          <textarea
            id="jobDescription"
            name="jobDescription"
            value={jobPost.jobDescription}
            onChange={handleChange}
            maxLength="5000"
          />
        </div>

        <div className="form-group">
          <label htmlFor="employerId">Employer ID:</label>
          <input
            type="number"
            id="employerId"
            name="employerId"
            value={jobPost.employerId}
            onChange={handleChange}
            required
          />
        </div>

        <div className="form-group">
          <label htmlFor="salary">Salary:</label>
          <input
            type="number"
            id="salary"
            name="salary"
            value={jobPost.salary}
            onChange={handleChange}
            required
          />
        </div>

        <div className="form-group">
          <label htmlFor="jobTitle">Job Title:</label>
          <input
            type="text"
            id="jobTitle"
            name="jobTitle"
            value={jobPost.jobTitle}
            onChange={handleChange}
            maxLength="100"
            required
          />
        </div>

        <div className="form-group">
          <label htmlFor="jobLocation">Job Location:</label>
          <input
            type="text"
            id="jobLocation"
            name="jobLocation"
            value={jobPost.jobLocation}
            onChange={handleChange}
            maxLength="255"
            required
          />
        </div>

        <div className="form-group">
          <label htmlFor="closeDate">Close Date:</label>
          <input
            type="date"
            id="closeDate"
            name="closeDate"
            value={jobPost.closeDate}
            onChange={handleChange}
          />
        </div>

        <button type="submit">{isEditing ? 'Update Job Posting' : 'Create Job Posting'}</button>
      </form>
    </div>
  );
};

export default JobPostings;
