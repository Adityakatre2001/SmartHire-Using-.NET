using AutoMapper;
using Microsoft.Extensions.Logging;
using SmartHire.DTOs;
using SmartHire.Models;
using SmartHire.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHire.Services
{
    public class JobPostingService : IJobPostingService
    {
        private readonly IMapper _mapper;
        private readonly ICompanyRepository _companyRepository;
        private readonly IJobPostingRepository _jobPostingRepository;
        private readonly ILogger<JobPostingService> _logger;

        public JobPostingService(
            IMapper mapper,
            ICompanyRepository companyRepository,
            IJobPostingRepository jobPostingRepository,
            ILogger<JobPostingService> logger)
        {
            _mapper = mapper;
            _companyRepository = companyRepository;
            _jobPostingRepository = jobPostingRepository;
            _logger = logger;
        }

        public async Task<JobPostDTO> CreateJobPostingAsync(JobPostDTO jobPostDTO)
        {
            try
            {
                var jobPosting = _mapper.Map<JobPosting>(jobPostDTO);
                var company = await _companyRepository.GetByIdAsync(jobPostDTO.EmployerId)
                    ?? throw new Exception("Company not found");

                jobPosting.Employer = company;

                var savedJobPosting = await _jobPostingRepository.CreateAsync(jobPosting);
                return _mapper.Map<JobPostDTO>(savedJobPosting);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating job posting.");
                throw;
            }
        }

        public async Task<JobPostDTO> UpdateJobPostingAsync(long jobId, JobPostDTO jobPostDTO)
        {
            var jobPosting = await _jobPostingRepository.GetByIdAsync(jobId)
                ?? throw new Exception("Job Posting not found");

            _mapper.Map(jobPostDTO, jobPosting);
            var updatedJobPosting = await _jobPostingRepository.UpdateAsync(jobPosting);
            return _mapper.Map<JobPostDTO>(updatedJobPosting);
        }

        public async Task DeleteJobPostingAsync(long jobId)
        {
            await _jobPostingRepository.DeleteAsync(jobId);
        }

        public async Task<JobPostDTO> GetJobPostingByIdAsync(long jobId)
        {
            var jobPosting = await _jobPostingRepository.GetByIdAsync(jobId)
                ?? throw new Exception("Job Posting not found");

            return _mapper.Map<JobPostDTO>(jobPosting);
        }

        public async Task<List<JobPostDTO>> GetAllJobPostingsAsync()
        {
            var jobPostings = await _jobPostingRepository.GetAllAsync();
            return jobPostings.Select(jobPosting => _mapper.Map<JobPostDTO>(jobPosting)).ToList();
        }

        public JobPostDTO CreateJobPosting(JobPostDTO jobPostingDTO)
        {
            throw new NotImplementedException();
        }

        public JobPostDTO UpdateJobPosting(long jobId, JobPostDTO jobPostingDTO)
        {
            throw new NotImplementedException();
        }

        public void DeleteJobPosting(long jobId)
        {
            throw new NotImplementedException();
        }

        public JobPostDTO GetJobPostingById(long jobId)
        {
            throw new NotImplementedException();
        }

        public List<JobPostDTO> GetAllJobPostings()
        {
            throw new NotImplementedException();
        }
    }
}
