using AutoMapper;
using SmartHire.DTOs;
using SmartHire.Models;
using SmartHire.Repositories;

namespace SmartHire.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly IMapper _mapper;
        private readonly IJobApplicationRepository _applicationRepository;

        public ApplicationService(IMapper mapper, IJobApplicationRepository applicationRepository)
        {
            _mapper = mapper;
            _applicationRepository = applicationRepository;
        }

        public async Task<ApplicantDTO> CreateApplicationAsync(ApplicantDTO applicantDTO)
        {
            // Convert DTO to Entity
            var jobApplication = _mapper.Map<JobApplication>(applicantDTO);
            // Save Entity
            var savedJobApplication = await _applicationRepository.AddAsync(jobApplication);
            // Convert saved Entity to DTO
            return _mapper.Map<ApplicantDTO>(savedJobApplication);
        }

        public async Task<ApplicantDTO> UpdateApplicationAsync(long applicationId, ApplicantDTO applicantDTO)
        {
            // Find existing Entity
            var jobApplication = await _applicationRepository.GetByIdAsync(applicationId);
            if (jobApplication == null)
            {
                throw new Exception("Application not found");
            }
            // Update Entity
            _mapper.Map(applicantDTO, jobApplication);
            // Save updated Entity
            var updatedJobApplication = await _applicationRepository.UpdateAsync(jobApplication);
            // Convert updated Entity to DTO
            return _mapper.Map<ApplicantDTO>(updatedJobApplication);
        }

        public async Task DeleteApplicationAsync(long applicationId)
        {
            await _applicationRepository.DeleteAsync(applicationId);
        }

        public async Task<ApplicantDTO> GetApplicationByIdAsync(long applicationId)
        {
            // Find existing Entity
            var jobApplication = await _applicationRepository.GetByIdAsync(applicationId);
            if (jobApplication == null)
            {
                throw new Exception("Application not found");
            }
            // Convert Entity to DTO
            return _mapper.Map<ApplicantDTO>(jobApplication);
        }

        public async Task<IEnumerable<ApplicantDTO>> GetAllApplicationsAsync()
        {
            var jobApplications = await _applicationRepository.GetAllAsync();
            return jobApplications.Select(ja => _mapper.Map<ApplicantDTO>(ja)).ToList();
        }
    }
}
