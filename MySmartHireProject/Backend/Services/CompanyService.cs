using AutoMapper;
using SmartHire.DTOs;
using SmartHire.Models;
using SmartHire.Repositories;

namespace SmartHire.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public CompanyService(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
        }

        public async Task<CompanyDTO> CreateCompanyAsync(CompanyDTO companyDTO)
        {
            var company = _mapper.Map<Company>(companyDTO);
            var savedCompany = await _companyRepository.CreateAsync(company);
            return _mapper.Map<CompanyDTO>(savedCompany);
        }

        public async Task<CompanyDTO> UpdateCompanyAsync(long companyId, CompanyDTO companyDTO)
        {
            var company = await _companyRepository.GetByIdAsync(companyId);
            if (company == null)
            {
                throw new Exception("Company not found");
            }

            _mapper.Map(companyDTO, company);
            var updatedCompany = await _companyRepository.UpdateAsync(company);
            return _mapper.Map<CompanyDTO>(updatedCompany);
        }

        public async Task DeleteCompanyAsync(long companyId)
        {
            var company = await _companyRepository.GetByIdAsync(companyId);
            if (company == null)
            {
                throw new Exception("Company not found");
            }

            await _companyRepository.DeleteAsync(companyId);
        }

     

        public async Task<CompanyDTO> GetCompanyByIdAsync(long companyId)
        {
            var company = await _companyRepository.GetByIdAsync(companyId);
            if (company == null)
            {
                throw new Exception("Company not found");
            }

            return _mapper.Map<CompanyDTO>(company);
        }

        public async Task<IEnumerable<CompanyDTO>> GetAllCompaniesAsync()
        {
            var companies = await _companyRepository.GetAllAsync();
            return companies.Select(c => _mapper.Map<CompanyDTO>(c));
        }

        public CompanyDTO CreateCompany(CompanyDTO companyDTO)
        {
            throw new NotImplementedException();
        }

        public CompanyDTO UpdateCompany(long companyId, CompanyDTO companyDTO)
        {
            throw new NotImplementedException();
        }

        public void DeleteCompany(long companyId)
        {
            throw new NotImplementedException();
        }

        public CompanyDTO GetCompanyById(long companyId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CompanyDTO> GetAllCompanies()
        {
            throw new NotImplementedException();
        }
    }
}
