/*using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SmartHire.DTOs;
using SmartHire.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SmartHire.Controllers
{
    [Route("admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public AdminController(IAdminService adminService, IUserService userService, IConfiguration configuration)
        {
            _adminService = adminService;
            _userService = userService;
            _configuration = configuration;
        }

        // User SignIn to generate JWT Token
        [HttpPost("signin")]
        public async Task<IActionResult> UserSignIn([FromBody] AuthDTO dto)
        {
            try
            {
                var user = await _userService.AuthenticateUserAsync(dto);
                if (user == null)
                {
                    return Unauthorized(new ApiResponse { Message = "Invalid credentials" });
                }

                // Create JWT token
                var token = GenerateJwtToken(user);
                return Ok(new { Message = "SignIn successful", Token = token });
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(new ApiResponse { Message = e.Message });
            }
            catch (Exception e)
            {
                return StatusCode(500, new ApiResponse { Message = $"Internal Server Error: {e.Message}" });
            }
        }

        // Method to generate JWT Token
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

        // Secured endpoints with JWT authentication

        [Authorize]
        [HttpPost("users")]
        public async Task<IActionResult> CreateUser([FromBody] UserDTO userDTO)
        {
            try
            {
                var createdUser = await _adminService.CreateUserAsync(userDTO);
                return CreatedAtAction(nameof(GetUserById), new { userId = createdUser.UserId }, new { Message = "User created successfully", User = createdUser });
            }
            catch (Exception e)
            {
                return StatusCode(500, new ApiResponse { Message = $"Failed to create user: {e.Message}" });
            }
        }

        [Authorize]
        [HttpPut("users/{userId}")]
        public async Task<IActionResult> UpdateUser(long userId, [FromBody] UserDTO userDTO)
        {
            try
            {
                var updatedUser = await _adminService.UpdateUserAsync(userId, userDTO);
                return Ok(new { Message = "User updated successfully", User = updatedUser });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new ApiResponse { Message = "User not found" });
            }
            catch (Exception e)
            {
                return StatusCode(500, new ApiResponse { Message = $"Failed to update user: {e.Message}" });
            }
        }

        [Authorize]
        [HttpDelete("users/{userId}")]
        public async Task<IActionResult> DeleteUser(long userId)
        {
            try
            {
                await _adminService.DeleteUserAsync(userId);
                return NoContent(); // No content, successful deletion
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new ApiResponse { Message = "User not found" });
            }
            catch (Exception e)
            {
                return StatusCode(500, new ApiResponse { Message = $"Failed to delete user: {e.Message}" });
            }
        }

        [Authorize]
        [HttpGet("users/{userId}")]
        public async Task<IActionResult> GetUserById(long userId)
        {
            try
            {
                var user = await _adminService.GetUserByIdAsync(userId);
                return Ok(new { Message = "User fetched successfully", User = user });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new ApiResponse { Message = "User not found" });
            }
            catch (Exception e)
            {
                return StatusCode(500, new ApiResponse { Message = $"Failed to fetch user: {e.Message}" });
            }
        }

        [Authorize]
        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await _adminService.GetAllUsersAsync();
                return Ok(new { Message = "Users fetched successfully", Users = users });
            }
            catch (Exception e)
            {
                return StatusCode(500, new ApiResponse { Message = $"Failed to fetch users: {e.Message}" });
            }
        }

        // Company Endpoints secured with JWT

        [Authorize]
        [HttpPost("companies")]
        public async Task<IActionResult> CreateCompany([FromBody] CompanyDTO companyDTO)
        {
            try
            {
                var createdCompany = await _adminService.CreateCompanyAsync(companyDTO);
                return CreatedAtAction(nameof(GetCompanyById), new { companyId = createdCompany.CompanyId }, new { Message = "Company created successfully", Company = createdCompany });
            }
            catch (Exception e)
            {
                return StatusCode(500, new ApiResponse { Message = $"Failed to create company: {e.Message}" });
            }
        }

        [Authorize]
        [HttpPut("companies/{companyId}")]
        public async Task<IActionResult> UpdateCompany(long companyId, [FromBody] CompanyDTO companyDTO)
        {
            try
            {
                var updatedCompany = await _adminService.UpdateCompanyAsync(companyId, companyDTO);
                return Ok(new { Message = "Company updated successfully", Company = updatedCompany });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new ApiResponse { Message = "Company not found" });
            }
            catch (Exception e)
            {
                return StatusCode(500, new ApiResponse { Message = $"Failed to update company: {e.Message}" });
            }
        }

        [Authorize]
        [HttpDelete("companies/{companyId}")]
        public async Task<IActionResult> DeleteCompany(long companyId)
        {
            try
            {
                await _adminService.DeleteCompanyAsync(companyId);
                return NoContent(); // No content, successful deletion
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new ApiResponse { Message = "Company not found" });
            }
            catch (Exception e)
            {
                return StatusCode(500, new ApiResponse { Message = $"Failed to delete company: {e.Message}" });
            }
        }

        [Authorize]
        [HttpGet("companies/{companyId}")]
        public async Task<IActionResult> GetCompanyById(long companyId)
        {
            try
            {
                var company = await _adminService.GetCompanyByIdAsync(companyId);
                return Ok(new { Message = "Company fetched successfully", Company = company });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new ApiResponse { Message = "Company not found" });
            }
            catch (Exception e)
            {
                return StatusCode(500, new ApiResponse { Message = $"Failed to fetch company: {e.Message}" });
            }
        }

        [Authorize]
        [HttpGet("companies")]
        public async Task<IActionResult> GetAllCompanies()
        {
            try
            {
                var companies = await _adminService.GetAllCompaniesAsync();
                return Ok(new { Message = "Companies fetched successfully", Companies = companies });
            }
            catch (Exception e)
            {
                return StatusCode(500, new ApiResponse { Message = $"Failed to fetch companies: {e.Message}" });
            }
        }
    }
}
*/


//***********************************************************************

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using SmartHire.DTOs;
using SmartHire.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SmartHire.Controllers
{
    [Route("admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly IMemoryCache _memoryCache;

        public AdminController(IAdminService adminService, IUserService userService, IConfiguration configuration, IMemoryCache memoryCache)
        {
            _adminService = adminService;
            _userService = userService;
            _configuration = configuration;
            _memoryCache = memoryCache;
        }

        // User SignIn to generate JWT Token
        [HttpPost("signin")] //: Maps HTTP POST requests to /admin/signin to this action.
        public async Task<IActionResult> UserSignIn([FromBody] AuthDTO dto)
        {
            try
            {
                var user = await _userService.AuthenticateUserAsync(dto);
                if (user == null)
                {
                    return Unauthorized(new ApiResponse { Message = "Invalid credentials" });
                }

                // Create JWT token
                var token = GenerateJwtToken(user);
                return Ok(new { Message = "SignIn successful", Token = token });
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(new ApiResponse { Message = e.Message });
            }
            catch (Exception e)
            {
                return StatusCode(500, new ApiResponse { Message = $"Internal Server Error: {e.Message}" });
            }
        }

        // Method to generate JWT Token
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

        // Cache keys
        private string GetUserCacheKey(long userId) => $"User_{userId}";
        private string GetAllUsersCacheKey() => "AllUsers";
        private string GetCompanyCacheKey(long companyId) => $"Company_{companyId}";
        private string GetAllCompaniesCacheKey() => "AllCompanies";

      /*  [Authorize]*/
        [HttpPost("users")]
        public async Task<IActionResult> CreateUser([FromBody] UserDTO userDTO)
        {
            try
            {
                var createdUser = await _adminService.CreateUserAsync(userDTO);

                // Clear cache after creating a user
                _memoryCache.Remove(GetAllUsersCacheKey());

                return CreatedAtAction(nameof(GetUserById), new { userId = createdUser.UserId }, new { Message = "User created successfully", User = createdUser });
            }
            catch (Exception e)
            {
                return StatusCode(500, new ApiResponse { Message = $"Failed to create user: {e.Message}" });
            }
        }

        [Authorize]
        [HttpPut("users/{userId}")]
        public async Task<IActionResult> UpdateUser(long userId, [FromBody] UserDTO userDTO)
        {
            try
            {
                var updatedUser = await _adminService.UpdateUserAsync(userId, userDTO);

                // Update cache for the user
                _memoryCache.Set(GetUserCacheKey(userId), updatedUser);
                _memoryCache.Remove(GetAllUsersCacheKey());

                return Ok(new { Message = "User updated successfully", User = updatedUser });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new ApiResponse { Message = "User not found" });
            }
            catch (Exception e)
            {
                return StatusCode(500, new ApiResponse { Message = $"Failed to update user: {e.Message}" });
            }
        }

        [Authorize]
        [HttpDelete("users/{userId}")]
        public async Task<IActionResult> DeleteUser(long userId)
        {
            try
            {
                await _adminService.DeleteUserAsync(userId);

                // Clear cache after deleting a user
                _memoryCache.Remove(GetUserCacheKey(userId));
                _memoryCache.Remove(GetAllUsersCacheKey());

                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new ApiResponse { Message = "User not found" });
            }
            catch (Exception e)
            {
                return StatusCode(500, new ApiResponse { Message = $"Failed to delete user: {e.Message}" });
            }
        }

        [Authorize]
        [HttpGet("users/{userId}")]
        public async Task<IActionResult> GetUserById(long userId)
        {
            try
            {
                if (!_memoryCache.TryGetValue(GetUserCacheKey(userId), out UserDTO user))
                {
                    user = await _adminService.GetUserByIdAsync(userId);

                    // Store user in cache
                    _memoryCache.Set(GetUserCacheKey(userId), user, TimeSpan.FromMinutes(30));
                }

                return Ok(new { Message = "User fetched successfully", User = user });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new ApiResponse { Message = "User not found" });
            }
            catch (Exception e)
            {
                return StatusCode(500, new ApiResponse { Message = $"Failed to fetch user: {e.Message}" });
            }
        }

        [Authorize]
        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                if (!_memoryCache.TryGetValue(GetAllUsersCacheKey(), out IEnumerable<UserDTO> users))
                {
                    users = await _adminService.GetAllUsersAsync();

                    // Store users in cache
                    _memoryCache.Set(GetAllUsersCacheKey(), users, TimeSpan.FromMinutes(30));
                }

                return Ok(new { Message = "Users fetched successfully", Users = users });
            }
            catch (Exception e)
            {
                return StatusCode(500, new ApiResponse { Message = $"Failed to fetch users: {e.Message}" });
            }
        }

        // Company endpoints with caching logic...

        [Authorize]
        [HttpPost("companies")]
        public async Task<IActionResult> CreateCompany([FromBody] CompanyDTO companyDTO)
        {
            try
            {
                var createdCompany = await _adminService.CreateCompanyAsync(companyDTO);

                // Clear cache after creating a company
                _memoryCache.Remove(GetAllCompaniesCacheKey());

                return CreatedAtAction(nameof(GetCompanyById), new { companyId = createdCompany.CompanyId }, new { Message = "Company created successfully", Company = createdCompany });
            }
            catch (Exception e)
            {
                return StatusCode(500, new ApiResponse { Message = $"Failed to create company: {e.Message}" });
            }
        }

        [Authorize]
        [HttpPut("companies/{companyId}")]
        public async Task<IActionResult> UpdateCompany(long companyId, [FromBody] CompanyDTO companyDTO)
        {
            try
            {
                var updatedCompany = await _adminService.UpdateCompanyAsync(companyId, companyDTO);

                // Update cache for the company
                _memoryCache.Set(GetCompanyCacheKey(companyId), updatedCompany);
                _memoryCache.Remove(GetAllCompaniesCacheKey());

                return Ok(new { Message = "Company updated successfully", Company = updatedCompany });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new ApiResponse { Message = "Company not found" });
            }
            catch (Exception e)
            {
                return StatusCode(500, new ApiResponse { Message = $"Failed to update company: {e.Message}" });
            }
        }

        [Authorize]
        [HttpDelete("companies/{companyId}")]
        public async Task<IActionResult> DeleteCompany(long companyId)
        {
            try
            {
                await _adminService.DeleteCompanyAsync(companyId);

                // Clear cache after deleting a company
                _memoryCache.Remove(GetCompanyCacheKey(companyId));
                _memoryCache.Remove(GetAllCompaniesCacheKey());

                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new ApiResponse { Message = "Company not found" });
            }
            catch (Exception e)
            {
                return StatusCode(500, new ApiResponse { Message = $"Failed to delete company: {e.Message}" });
            }
        }

        [Authorize]
        [HttpGet("companies/{companyId}")]
        public async Task<IActionResult> GetCompanyById(long companyId)
        {
            try
            {
                if (!_memoryCache.TryGetValue(GetCompanyCacheKey(companyId), out CompanyDTO company))
                {
                    company = await _adminService.GetCompanyByIdAsync(companyId);

                    // Store company in cache
                    _memoryCache.Set(GetCompanyCacheKey(companyId), company, TimeSpan.FromMinutes(30));
                }

                return Ok(new { Message = "Company fetched successfully", Company = company });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new ApiResponse { Message = "Company not found" });
            }
            catch (Exception e)
            {
                return StatusCode(500, new ApiResponse { Message = $"Failed to fetch company: {e.Message}" });
            }
        }

        [Authorize]
        [HttpGet("companies")]
        public async Task<IActionResult> GetAllCompanies()
        {
            try
            {
                if (!_memoryCache.TryGetValue(GetAllCompaniesCacheKey(), out IEnumerable<CompanyDTO> companies))
                {
                    companies = await _adminService.GetAllCompaniesAsync();

                    // Store companies in cache
                    _memoryCache.Set(GetAllCompaniesCacheKey(), companies, TimeSpan.FromMinutes(30));
                }

                return Ok(new { Message = "Companies fetched successfully", Companies = companies });
            }
            catch (Exception e)
            {
                return StatusCode(500, new ApiResponse { Message = $"Failed to fetch companies: {e.Message}" });
            }
        }
    }
}
