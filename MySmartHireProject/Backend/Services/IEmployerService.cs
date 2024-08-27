using System.Collections.Generic;

using SmartHire.DTOs;

namespace SmartHire.Services
{
    public interface IEmployerService
    {
        JobPostDTO CreateJobPosting(JobPostDTO jobPostDto);
        JobPostDTO UpdateJobPosting(long jobId, JobPostDTO jobPostDto);
        void DeleteJobPosting(long jobId);
        JobPostDTO GetJobPostingById(long jobId);
        IEnumerable<JobPostDTO> GetAllJobPostings();
        IEnumerable<ApplicantDTO> GetApplicationsForJob(long jobId);
    }
}
