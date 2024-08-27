using System.Collections.Generic;
using System.Threading.Tasks;
using SmartHire.Models;

namespace SmartHire.Repositories
{
    public interface IJobApplicationRepository
    {
        Task<JobApplication> GetByIdAsync(long jobApplicationId);
        Task<JobApplication> AddAsync(JobApplication jobApplication);
        Task<JobApplication> UpdateAsync(JobApplication jobApplication);
        Task DeleteAsync(long jobApplicationId);
        Task<IEnumerable<JobApplication>> GetAllAsync(); // Add this method
    }
}
