using System.Collections.Generic;
using SmartHire.DTOs;

public interface IJobPostingService
{
    JobPostDTO CreateJobPosting(JobPostDTO jobPostingDTO);
    JobPostDTO UpdateJobPosting(long jobId, JobPostDTO jobPostingDTO);
    void DeleteJobPosting(long jobId);
    JobPostDTO GetJobPostingById(long jobId);
    List<JobPostDTO> GetAllJobPostings();
}
