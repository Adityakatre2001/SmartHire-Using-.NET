

using SmartHire.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartHire.Services
{
    public interface IEmployerService
    {
        Task<JobPostDTO> CreateJobPostingAsync(JobPostDTO jobPostDto);
        Task<JobPostDTO> UpdateJobPostingAsync(long jobId, JobPostDTO jobPostDto);
        Task DeleteJobPostingAsync(long jobId);
        Task<JobPostDTO> GetJobPostingByIdAsync(long jobId);
        Task<IEnumerable<JobPostDTO>> GetAllJobPostingsAsync();
        Task<IEnumerable<ApplicantDTO>> GetApplicationsForJobAsync(long jobId);
    }
}
