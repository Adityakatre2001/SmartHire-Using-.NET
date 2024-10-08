/*
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartHire.DTOs;
using SmartHire.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Memory;

namespace SmartHire.Controllers
{
    [Route("users")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IApplicationService _applicationService;
        private readonly IConfiguration _configuration;
        private readonly IMemoryCache _memoryCache; // Inject IMemoryCache for in-memory caching
        private const string ApplicationCacheKey = "applicationsCacheKey";

        public UserController(IUserService userService, IApplicationService applicationService, IConfiguration configuration, IMemoryCache memoryCache)
        {
            _userService = userService;
            _applicationService = applicationService;
            _configuration = configuration;
            _memoryCache = memoryCache; // Initialize the cache
        }

        // Login and generate JWT token
        [HttpPost("signin")]
        public async Task<IActionResult> UserSignIn([FromBody] AuthDTO dto)
        {
            try
            {
                var user = await _userService.AuthenticateUserAsync(dto);

                if (user == null)
                {
                    return Unauthorized(new { message = "Invalid credentials" });
                }

                var token = GenerateJwtToken(user);
                return Ok(new { token });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred during sign-in", error = ex.Message });
            }
        }

        private string GenerateJwtToken(UserDTO user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("UserId", user.UserId.ToString()),  // Assuming UserDTO has a UserId property
                new Claim("Role", user.Role) // Add user's role if applicable
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // Securing Application Management routes using JWT Authorization
        [Authorize]
        [HttpPost("applications")]
        public async Task<IActionResult> CreateApplicationAsync([FromBody] ApplicantDTO applicationDTO)
        {
            try
            {
                var createdApplication = await _applicationService.CreateApplicationAsync(applicationDTO);
                return Ok(createdApplication);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new { message = "Database error occurred while creating the application", error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred while creating the application", error = ex.Message });
            }
        }

        [Authorize]
        [HttpPut("applications/{applicationId}")]
        public async Task<IActionResult> UpdateApplicationAsync([FromRoute] long applicationId, [FromBody] ApplicantDTO applicationDTO)
        {
            try
            {
                var updatedApplication = await _applicationService.UpdateApplicationAsync(applicationId, applicationDTO);
                return Ok(updatedApplication);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "Application not found" });
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new { message = "Database error occurred while updating the application", error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred while updating the application", error = ex.Message });
            }
        }

        [Authorize]
        [HttpDelete("applications/{applicationId}")]
        public async Task<IActionResult> DeleteApplicationAsync([FromRoute] long applicationId)
        {
            try
            {
                await _applicationService.DeleteApplicationAsync(applicationId);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "Application not found" });
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new { message = "Database error occurred while deleting the application", error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred while deleting the application", error = ex.Message });
            }
        }

        [Authorize]
        [HttpGet("applications/{applicationId}")]
        public async Task<IActionResult> GetApplicationByIdAsync([FromRoute] long applicationId)
        {
            try
            {
                var applicationDTO = await _applicationService.GetApplicationByIdAsync(applicationId);

                if (applicationDTO == null)
                {
                    return NotFound(new { message = "Application not found" });
                }

                return Ok(applicationDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred while fetching the application", error = ex.Message });
            }
        }

        [Authorize]
        [HttpGet("applications")]
        public async Task<IActionResult> GetAllApplicationsAsync()
        {
            try
            {
                var applications = await _applicationService.GetAllApplicationsAsync();
                return Ok(applications);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred while fetching applications", error = ex.Message });
            }
        }
    }
}
*/

/*
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartHire.DTOs;
using SmartHire.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Memory;

namespace SmartHire.Controllers
{
    [Route("users")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IApplicationService _applicationService;
        private readonly IConfiguration _configuration;
        private readonly IMemoryCache _memoryCache; // Inject IMemoryCache for caching
        private const string ApplicationsCacheKey = "applicationsCacheKey"; // Cache key for applications

        public UserController(IUserService userService, IApplicationService applicationService, IConfiguration configuration, IMemoryCache memoryCache)
        {
            _userService = userService;
            _applicationService = applicationService;
            _configuration = configuration;
            _memoryCache = memoryCache; // Initialize cache service
        }

        // Login and generate JWT token
        [HttpPost("signin")]
        public async Task<IActionResult> UserSignIn([FromBody] AuthDTO dto)
        {
            try
            {
                var user = await _userService.AuthenticateUserAsync(dto);

                if (user == null)
                {
                    return Unauthorized(new { message = "Invalid credentials" });
                }

                var token = GenerateJwtToken(user);
                return Ok(new { token });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred during sign-in", error = ex.Message });
            }
        }

        private string GenerateJwtToken(UserDTO user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("UserId", user.UserId.ToString()),  // Assuming UserDTO has a UserId property
                new Claim("Role", user.Role) // Add user's role if applicable
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // Caching applications with GetAllApplicationsAsync
        [Authorize]
        [HttpGet("applications")]
        public async Task<IActionResult> GetAllApplicationsAsync()
        {
            try
            {
                // Check if applications are cached
                if (!_memoryCache.TryGetValue(ApplicationsCacheKey, out IEnumerable<ApplicantDTO> cachedApplications))
                {
                    // If not in cache, fetch from service
                    var applications = await _applicationService.GetAllApplicationsAsync();

                    // Set cache options
                    var cacheEntryOptions = new MemoryCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10), // Cache for 10 minutes
                        SlidingExpiration = TimeSpan.FromMinutes(2) // Refresh if accessed within 2 minutes
                    };

                    // Cache the result
                    _memoryCache.Set(ApplicationsCacheKey, applications, cacheEntryOptions);

                    return Ok(applications);
                }

                // Return cached applications
                return Ok(cachedApplications);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred while fetching applications", error = ex.Message });
            }
        }

        // Clear cache when adding or updating an application
        [Authorize]
        [HttpPost("applications")]
        public async Task<IActionResult> CreateApplicationAsync([FromBody] ApplicantDTO applicationDTO)
        {
            try
            {
                var createdApplication = await _applicationService.CreateApplicationAsync(applicationDTO);

                // Invalidate cache after creating a new application
                _memoryCache.Remove(ApplicationsCacheKey);

                return Ok(createdApplication);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new { message = "Database error occurred while creating the application", error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred while creating the application", error = ex.Message });
            }
        }

        [Authorize]
        [HttpPut("applications/{applicationId}")]
        public async Task<IActionResult> UpdateApplicationAsync([FromRoute] long applicationId, [FromBody] ApplicantDTO applicationDTO)
        {
            try
            {
                var updatedApplication = await _applicationService.UpdateApplicationAsync(applicationId, applicationDTO);

                // Invalidate cache after updating the application
                _memoryCache.Remove(ApplicationsCacheKey);

                return Ok(updatedApplication);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "Application not found" });
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new { message = "Database error occurred while updating the application", error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred while updating the application", error = ex.Message });
            }
        }
    }
}*/

//**************************************************************************

using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartHire.DTOs;
using SmartHire.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Memory;

namespace SmartHire.Controllers
{
    [Route("users")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IApplicationService _applicationService;
        private readonly IConfiguration _configuration;
        private readonly IMemoryCache _memoryCache; // Inject IMemoryCache for caching
        private const string ApplicationsCacheKey = "applicationsCacheKey"; // Cache key for applications

        public UserController(IUserService userService, IApplicationService applicationService, IConfiguration configuration, IMemoryCache memoryCache)
        {
            _userService = userService;
            _applicationService = applicationService;
            _configuration = configuration;
            _memoryCache = memoryCache; // Initialize cache service
        }

        // Login and generate JWT token
        [HttpPost("signin")]
        public async Task<IActionResult> UserSignIn([FromBody] AuthDTO dto)
        {
            try
            {
                var user = await _userService.AuthenticateUserAsync(dto);

                if (user == null)
                {
                    return Unauthorized(new { message = "Invalid credentials" });
                }

                var token = GenerateJwtToken(user);
                return Ok(new { token });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred during sign-in", error = ex.Message });
            }
        }

        private string GenerateJwtToken(UserDTO user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("UserId", user.UserId.ToString()),  // Assuming UserDTO has a UserId property
                new Claim("Role", user.Role) // Add user's role if applicable
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // Caching applications with GetAllApplicationsAsync
        [Authorize]
        [HttpGet("applications")]
        public async Task<IActionResult> GetAllApplicationsAsync()
        {
            try
            {
                // Check if applications are cached
                if (!_memoryCache.TryGetValue(ApplicationsCacheKey, out IEnumerable<ApplicantDTO> cachedApplications))
                {
                    // If not in cache, fetch from service
                    var applications = await _applicationService.GetAllApplicationsAsync();

                    // Set cache options
                    var cacheEntryOptions = new MemoryCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10), // Cache for 10 minutes
                        SlidingExpiration = TimeSpan.FromMinutes(2) // Refresh if accessed within 2 minutes
                    };

                    // Cache the result
                    _memoryCache.Set(ApplicationsCacheKey, applications, cacheEntryOptions);

                    return Ok(applications);
                }

                // Return cached applications
                return Ok(cachedApplications);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred while fetching applications", error = ex.Message });
            }
        }

        // Clear cache when adding or updating an application
        [Authorize]
        [HttpPost("applications")]
        public async Task<IActionResult> CreateApplicationAsync([FromBody] ApplicantDTO applicationDTO)
        {
            try
            {
                var createdApplication = await _applicationService.CreateApplicationAsync(applicationDTO);

                // Invalidate cache after creating a new application
                _memoryCache.Remove(ApplicationsCacheKey);

                return Ok(createdApplication);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new { message = "Database error occurred while creating the application", error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred while creating the application", error = ex.Message });
            }
        }

        [Authorize]
        [HttpPut("applications/{applicationId}")]
        public async Task<IActionResult> UpdateApplicationAsync([FromRoute] long applicationId, [FromBody] ApplicantDTO applicationDTO)
        {
            try
            {
                var updatedApplication = await _applicationService.UpdateApplicationAsync(applicationId, applicationDTO);

                // Invalidate cache after updating the application
                _memoryCache.Remove(ApplicationsCacheKey);

                return Ok(updatedApplication);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "Application not found" });
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new { message = "Database error occurred while updating the application", error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred while updating the application", error = ex.Message });
            }
        }
    }
}











