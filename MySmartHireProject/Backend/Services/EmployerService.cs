using AutoMapper;
using Microsoft.Extensions.Logging;
using SmartHire.DTOs;
using SmartHire.Models;
using SmartHire.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHire.Services
{
    public class EmployerService : IEmployerService
    {
        private readonly IJobPostingRepository _jobPostingRepository;
        private readonly IJobApplicationRepository _jobApplicationRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<EmployerService> _logger;

        public EmployerService(
            IJobPostingRepository jobPostingRepository,
            IJobApplicationRepository jobApplicationRepository,
            IMapper mapper,
            ILogger<EmployerService> logger)
        {
            _jobPostingRepository = jobPostingRepository;
            _jobApplicationRepository = jobApplicationRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<JobPostDTO> CreateJobPostingAsync(JobPostDTO jobPostDto)
        {
            var jobPosting = _mapper.Map<JobPosting>(jobPostDto);
            var savedJobPosting = await _jobPostingRepository.CreateAsync(jobPosting);
            return _mapper.Map<JobPostDTO>(savedJobPosting);
        }

        public async Task<JobPostDTO> UpdateJobPostingAsync(long jobId, JobPostDTO jobPostDto)
        {
            var jobPosting = await _jobPostingRepository.GetByIdAsync(jobId);
            if (jobPosting == null)
            {
                throw new KeyNotFoundException("Job posting not found");
            }

            _mapper.Map(jobPostDto, jobPosting);
            var updatedJobPosting = await _jobPostingRepository.UpdateAsync(jobPosting);
            return _mapper.Map<JobPostDTO>(updatedJobPosting);
        }

        public async Task DeleteJobPostingAsync(long jobId)
        {
            await _jobPostingRepository.DeleteAsync(jobId);
        }

        public async Task<JobPostDTO> GetJobPostingByIdAsync(long jobId)
        {
            var jobPosting = await _jobPostingRepository.GetByIdAsync(jobId);
            if (jobPosting == null)
            {
                throw new KeyNotFoundException("Job posting not found");
            }
            return _mapper.Map<JobPostDTO>(jobPosting);
        }

        public async Task<IEnumerable<JobPostDTO>> GetAllJobPostingsAsync()
        {
            var jobPostings = await _jobPostingRepository.GetAllAsync();
            return jobPostings.Select(jp => _mapper.Map<JobPostDTO>(jp)).ToList();
        }

        public async Task<IEnumerable<ApplicantDTO>> GetApplicationsForJobAsync(long jobId)
        {
            var applications = await _jobApplicationRepository.GetAllAsync();
            var filteredApplications = applications.Where(ja => ja.JobPostingId == jobId);
            return filteredApplications.Select(ja => _mapper.Map<ApplicantDTO>(ja)).ToList();
        }

        public JobPostDTO CreateJobPosting(JobPostDTO jobPostDto)
        {
            throw new NotImplementedException();
        }

        public JobPostDTO UpdateJobPosting(long jobId, JobPostDTO jobPostDto)
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

        public IEnumerable<JobPostDTO> GetAllJobPostings()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ApplicantDTO> GetApplicationsForJob(long jobId)
        {
            throw new NotImplementedException();
        }
    }
}
