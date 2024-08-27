using Microsoft.EntityFrameworkCore;
using SmartHire.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartHire.Repositories
{
    public class JobPostingRepository : IJobPostingRepository
    {
        private readonly ApplicationDbContext _context;

        public JobPostingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<JobPosting> GetByIdAsync(long jobId)
        {
            return await _context.JobPostings.FindAsync(jobId);
        }

        public async Task<JobPosting> CreateAsync(JobPosting jobPosting)
        {
            _context.JobPostings.Add(jobPosting);
            await _context.SaveChangesAsync();
            return jobPosting;
        }

        public async Task<JobPosting> UpdateAsync(JobPosting jobPosting)
        {
            _context.JobPostings.Update(jobPosting);
            await _context.SaveChangesAsync();
            return jobPosting;
        }

        public async Task DeleteAsync(long jobId)
        {
            var jobPosting = await _context.JobPostings.FindAsync(jobId);
            if (jobPosting != null)
            {
                _context.JobPostings.Remove(jobPosting);
                await _context.SaveChangesAsync();
            }
        }

        // Implement GetAllAsync
        public async Task<IEnumerable<JobPosting>> GetAllAsync()
        {
            return await _context.JobPostings.ToListAsync();
        }
    }
}
