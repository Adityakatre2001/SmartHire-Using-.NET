using AutoMapper;
using SmartHire.DTOs;
using SmartHire.Models;
using System;

public class MappingProfile : AutoMapper.Profile
{
    public MappingProfile()
    {
        // Mapping for User and UserDTO
        CreateMap<UserDTO, User>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => MapRole(src.Role)))  // Map User Role
            .ReverseMap()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.ToString()));

        // Mapping for Company and CompanyDTO
        CreateMap<CompanyDTO, Company>().ReverseMap();  // Map Company entity to DTO and vice versa

        CreateMap<JobPostDTO, JobPosting>().ReverseMap();

        CreateMap<ApplicantDTO, JobApplication>();
    }

    private UserRole MapRole(string roleString)
    {
        
        if (Enum.TryParse<UserRole>(roleString, true, out var role))
        {
            return role;
        }

        
        throw new ArgumentException($"Invalid role value: {roleString}");
    }
}

