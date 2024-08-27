using System.Collections.Generic;
using SmartHire.DTOs;

namespace SmartHire.Services
{
    public interface ICompanyService
    {
        CompanyDTO CreateCompany(CompanyDTO companyDTO);
        CompanyDTO UpdateCompany(long companyId, CompanyDTO companyDTO);
        void DeleteCompany(long companyId);
        CompanyDTO GetCompanyById(long companyId);
        IEnumerable<CompanyDTO> GetAllCompanies();
    }
}
