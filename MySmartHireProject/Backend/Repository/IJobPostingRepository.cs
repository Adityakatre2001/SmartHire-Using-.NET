using SmartHire.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartHire.Repositories
{
    public interface IJobPostingRepository
    {
        Task<JobPosting> GetByIdAsync(long jobId);
        Task<JobPosting> CreateAsync(JobPosting jobPosting);
        Task<JobPosting> UpdateAsync(JobPosting jobPosting);
        Task DeleteAsync(long jobId);
        Task<IEnumerable<JobPosting>> GetAllAsync();  // Add this method
    }
}
