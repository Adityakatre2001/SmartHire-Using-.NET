using Microsoft.EntityFrameworkCore;
using SmartHire.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartHire.Repositories
{
    public class JobApplicationRepository : IJobApplicationRepository
    {
        private readonly ApplicationDbContext _context;

        public JobApplicationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<JobApplication> GetByIdAsync(long jobApplicationId)
        {
            return await _context.JobApplications.FindAsync(jobApplicationId);
        }

        public async Task<JobApplication> AddAsync(JobApplication jobApplication)
        {
            _context.JobApplications.Add(jobApplication);
            await _context.SaveChangesAsync();
            return jobApplication;
        }

        public async Task<JobApplication> UpdateAsync(JobApplication jobApplication)
        {
            _context.JobApplications.Update(jobApplication);
            await _context.SaveChangesAsync();
            return jobApplication;
        }

        public async Task DeleteAsync(long jobApplicationId)
        {
            var jobApplication = await _context.JobApplications.FindAsync(jobApplicationId);
            if (jobApplication != null)
            {
                _context.JobApplications.Remove(jobApplication);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<JobApplication>> GetAllAsync() // Implement the method
        {
            return await _context.JobApplications.ToListAsync();
        }
    }
}
