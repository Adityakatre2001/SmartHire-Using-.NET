using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SmartHire.DTOs;
using SmartHire.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Cors;

namespace SmartHire.Controllers
{
    [Route("employers")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    [EnableCors("AllowAll")]
    public class EmployerController : ControllerBase
    {
        private readonly IEmployerService _employerService;
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly IMemoryCache _memoryCache;

        public EmployerController(IEmployerService employerService, IUserService userService, IConfiguration configuration, IMemoryCache memoryCache)
        {
            _employerService = employerService;
            _userService = userService;
            _configuration = configuration;
            _memoryCache = memoryCache;
        }

        // Cache Keys
        private string GetJobCacheKey(long jobId) => $"Job_{jobId}";
        private string GetAllJobsCacheKey() => "AllJobs";
        private string GetApplicationsCacheKey(long jobId) => $"Applications_{jobId}";

        // Employer SignIn to generate JWT Token
        [HttpPost("signin")]
        public async Task<IActionResult> EmployerSignIn([FromBody] AuthDTO dto)
        {
            try
            {
                var user = await _userService.AuthenticateUserAsync(dto);
                if (user == null)
                {
                    return Unauthorized(new ApiResponse { Message = "Invalid credentials" });
                }

                if (user.Role != "Employer")
                {
                    return StatusCode(403, new ApiResponse { Message = "Access denied. Only Employer are allowed to sign in." });
                }


                // Create JWT token
                var token = GenerateJwtToken(user);
                return Ok(new ApiResponse { Message = "Sign-in successful", Data = new { Token = token } });
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(new ApiResponse { Message = e.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse { Message = $"An error occurred: {ex.Message}" });
            }
        }

        private string GenerateJwtToken(UserDTO user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("UserId", user.UserId.ToString()),
                new Claim("Role", user.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // Job Posting Management
        [Authorize(Roles = "Employer")]
        [HttpPost("jobs")]
        public async Task<IActionResult> CreateJobPosting([FromBody] JobPostDTO jobPostingDTO)
        {
            try
            {
                var createdJobPosting = await _employerService.CreateJobPostingAsync(jobPostingDTO);

                // Invalidate cache for all jobs
                _memoryCache.Remove(GetAllJobsCacheKey());

                return Ok(new ApiResponse { Message = "Job posting created successfully", Data = createdJobPosting });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse { Message = $"An error occurred: {ex.Message}" });
            }
        }

        [Authorize(Roles = "Employer")]
        [HttpPut("jobs/{jobId}")]
        public async Task<IActionResult> UpdateJobPosting([FromRoute] long jobId, [FromBody] JobPostDTO jobPostingDTO)
        {
            try
            {
                var updatedJobPosting = await _employerService.UpdateJobPostingAsync(jobId, jobPostingDTO);

                // Invalidate cache for the specific job and all jobs
                _memoryCache.Remove(GetJobCacheKey(jobId));
                _memoryCache.Remove(GetAllJobsCacheKey());

                return Ok(new ApiResponse { Message = "Job posting updated successfully", Data = updatedJobPosting });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new ApiResponse { Message = "Job posting not found" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse { Message = $"An error occurred: {ex.Message}" });
            }
        }

        [Authorize(Roles = "Employer")]
        [HttpDelete("jobs/{jobId}")]
        public async Task<IActionResult> DeleteJobPosting([FromRoute] long jobId)
        {
            try
            {
                await _employerService.DeleteJobPostingAsync(jobId);

                // Invalidate cache for the specific job and all jobs
                _memoryCache.Remove(GetJobCacheKey(jobId));
                _memoryCache.Remove(GetAllJobsCacheKey());

                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new ApiResponse { Message = "Job posting not found" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse { Message = $"An error occurred: {ex.Message}" });
            }
        }

        [Authorize(Roles = "Employer")]
        [HttpGet("jobs/{jobId}")]
        public async Task<IActionResult> GetJobPostingById([FromRoute] long jobId)
        {
            try
            {
                if (!_memoryCache.TryGetValue(GetJobCacheKey(jobId), out JobPostDTO jobPostingDTO))
                {
                    jobPostingDTO = await _employerService.GetJobPostingByIdAsync(jobId);

                    // Store job posting in cache
                    _memoryCache.Set(GetJobCacheKey(jobId), jobPostingDTO, TimeSpan.FromMinutes(30));
                }

                return Ok(new ApiResponse { Data = jobPostingDTO });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new ApiResponse { Message = "Job posting not found" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse { Message = $"An error occurred: {ex.Message}" });
            }
        }

        [Authorize(Roles = "Employer")]
        [HttpGet("jobs")]
        public async Task<IActionResult> GetAllJobPostings()
        {
            try
            {
                if (!_memoryCache.TryGetValue(GetAllJobsCacheKey(), out IEnumerable<JobPostDTO> jobPostings))
                {
                    jobPostings = await _employerService.GetAllJobPostingsAsync();

                    // Store all job postings in cache
                    _memoryCache.Set(GetAllJobsCacheKey(), jobPostings, TimeSpan.FromMinutes(30));
                }

                return Ok(new ApiResponse { Data = jobPostings });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse { Message = $"An error occurred: {ex.Message}" });
            }
        }

        /*  // Application Management
          [Authorize]
          [HttpGet("jobs/{jobId}/applications")]
          public async Task<IActionResult> GetApplicationsForJob([FromRoute] long jobId)
          {
              try
              {
                  if (!_memoryCache.TryGetValue(GetApplicationsCacheKey(jobId), out IEnumerable<JobApplicationDTO> applications))
                  {
                      applications = await _employerService.GetApplicationsForJobAsync(jobId);

                      // Store applications in cache
                      _memoryCache.Set(GetApplicationsCacheKey(jobId), applications, TimeSpan.FromMinutes(30));
                  }

                  return Ok(new ApiResponse { Data = applications });
              }
              catch (KeyNotFoundException)
              {
                  return NotFound(new ApiResponse { Message = "Job posting not found" });
              }
              catch (Exception ex)
              {
                  return StatusCode(500, new ApiResponse { Message = $"An error occurred: {ex.Message}" });
              }
          }*/
        [Authorize(Roles = "Employer")]
        [HttpGet("jobs/{jobId}/applications")]
        public async Task<IActionResult> GetApplicationsForJob([FromRoute] long jobId)
        {
            try
            {
                if (!_memoryCache.TryGetValue(GetApplicationsCacheKey(jobId), out IEnumerable<JobApplicationDTO> applications))
                {
                    var applicantDTOs = await _employerService.GetApplicationsForJobAsync(jobId);


                    // Store applications in cache
                    _memoryCache.Set(GetApplicationsCacheKey(jobId), applications, TimeSpan.FromMinutes(30));
                }

                return Ok(new ApiResponse { Data = applications });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new ApiResponse { Message = "Job posting not found" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse { Message = $"An error occurred: {ex.Message}" });
            }
        }




    }
}
