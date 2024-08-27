using SmartHire.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartHire.Services
{
    public interface IApplicationService
    {
        Task<ApplicantDTO> CreateApplicationAsync(ApplicantDTO applicationDTO);
        Task<ApplicantDTO> UpdateApplicationAsync(long applicationId, ApplicantDTO applicationDTO);
        Task DeleteApplicationAsync(long applicationId);
        Task<ApplicantDTO> GetApplicationByIdAsync(long applicationId);
        Task<IEnumerable<ApplicantDTO>> GetAllApplicationsAsync();
    }
}
