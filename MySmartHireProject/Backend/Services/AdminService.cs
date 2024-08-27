using AutoMapper;
using SmartHire.DTOs;
using SmartHire.Models;
using SmartHire.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHire.Services
{
    public class AdminService : IAdminService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly ICompanyRepository _companyRepository;

        // Constructor with dependencies
        public AdminService(IMapper mapper, IUserRepository userRepository, ICompanyRepository companyRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _companyRepository = companyRepository;
        }

        // Create User
        public async Task<UserDTO> CreateUserAsync(UserDTO userDTO)
        {
            var user = _mapper.Map<User>(userDTO);  // Mapping DTO to User entity
            var savedUser = await _userRepository.AddAsync(user);  // Saving the user
            return _mapper.Map<UserDTO>(savedUser);  // Mapping saved user back to DTO
        }

        // Update User
        public async Task<UserDTO> UpdateUserAsync(long userId, UserDTO userDTO)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            _mapper.Map(userDTO, user);  // Mapping the updated fields to the existing user
            var updatedUser = await _userRepository.UpdateAsync(user);  // Updating the user
            return _mapper.Map<UserDTO>(updatedUser);  // Mapping updated user back to DTO
        }

        // Delete User
        public async Task DeleteUserAsync(long userId)
        {
            await _userRepository.DeleteAsync(userId);  // Deleting the user by ID
        }

        // Get User by ID
        public async Task<UserDTO> GetUserByIdAsync(long userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            return _mapper.Map<UserDTO>(user);  // Mapping the user entity to DTO
        }

        // Get All Users
        public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return users.Select(user => _mapper.Map<UserDTO>(user)).ToList();  // Mapping all users to DTOs
        }

        // Company Management

        // Create Company
        public async Task<CompanyDTO> CreateCompanyAsync(CompanyDTO companyDTO)
        {
            var company = _mapper.Map<Company>(companyDTO);  // Mapping DTO to Company entity
            var savedCompany = await _companyRepository.CreateAsync(company);  // Saving the company
            return _mapper.Map<CompanyDTO>(savedCompany);  // Mapping saved company back to DTO
        }

        // Update Company
        public async Task<CompanyDTO> UpdateCompanyAsync(long companyId, CompanyDTO companyDTO)
        {
            var company = await _companyRepository.GetByIdAsync(companyId);
            if (company == null)
            {
                throw new Exception("Company not found");
            }

            _mapper.Map(companyDTO, company);  // Mapping the updated fields to the existing company
            var updatedCompany = await _companyRepository.UpdateAsync(company);  // Updating the company
            return _mapper.Map<CompanyDTO>(updatedCompany);  // Mapping updated company back to DTO
        }

        // Delete Company
        public async Task DeleteCompanyAsync(long companyId)
        {
            await _companyRepository.DeleteAsync(companyId);  // Deleting the company by ID
        }

        // Get Company by ID
        public async Task<CompanyDTO> GetCompanyByIdAsync(long companyId)
        {
            var company = await _companyRepository.GetByIdAsync(companyId);
            if (company == null)
            {
                throw new Exception("Company not found");
            }
            return _mapper.Map<CompanyDTO>(company);  // Mapping the company entity to DTO
        }

        // Get All Companies
        public async Task<IEnumerable<CompanyDTO>> GetAllCompaniesAsync()
        {
            var companies = await _companyRepository.GetAllAsync();
            return companies.Select(company => _mapper.Map<CompanyDTO>(company)).ToList();  // Mapping all companies to DTOs
        }
    }
}
