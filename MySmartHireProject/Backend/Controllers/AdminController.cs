using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SmartHire.DTOs;
using SmartHire.Services;

namespace SmartHire.Controllers
{
    [Route("admin")]
    [ApiController]
   // [EnableCors("AllowAll")] // Ensure CORS is enabled globally or via policy
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly IUserService _userService;

        public AdminController(IAdminService adminService, IUserService userService)
        {
            _adminService = adminService;
            _userService = userService;
        }

        [HttpPost("signin")]
        public async Task<IActionResult> UserSignIn([FromBody] AuthDTO dto)
        {
            try
            {
                var respDto = await _userService.AuthenticateUserAsync(dto);
                return Ok(respDto);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(new ApiResponse { Message = e.Message });
            }
        }

        [HttpPost("users")]
        public async Task<IActionResult> CreateUser([FromBody] UserDTO userDTO)
        {
            var createdUser = await _adminService.CreateUserAsync(userDTO);
            return CreatedAtAction(nameof(GetUserById), new { userId = createdUser.UserId }, createdUser);
        }

        [HttpPut("users/{userId}")]
        public async Task<IActionResult> UpdateUser(long userId, [FromBody] UserDTO userDTO)
        {
            var updatedUser = await _adminService.UpdateUserAsync(userId, userDTO);
            return Ok(updatedUser);
        }

        [HttpDelete("users/{userId}")]
        public async Task<IActionResult> DeleteUser(long userId)
        {
            await _adminService.DeleteUserAsync(userId);
            return NoContent();
        }

        [HttpGet("users/{userId}")]
        public async Task<IActionResult> GetUserById(long userId)
        {
            var user = await _adminService.GetUserByIdAsync(userId);
            return Ok(user);
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _adminService.GetAllUsersAsync();
            return Ok(users);
        }

        // Company Endpoints

        [HttpPost("companies")]
        public async Task<IActionResult> CreateCompany([FromBody] CompanyDTO companyDTO)
        {
            var createdCompany = await _adminService.CreateCompanyAsync(companyDTO);
            return CreatedAtAction(nameof(GetCompanyById), new { companyId = createdCompany.CompanyId }, createdCompany);
        }

        [HttpPut("companies/{companyId}")]
        public async Task<IActionResult> UpdateCompany(long companyId, [FromBody] CompanyDTO companyDTO)
        {
            var updatedCompany = await _adminService.UpdateCompanyAsync(companyId, companyDTO);
            return Ok(updatedCompany);
        }

        [HttpDelete("companies/{companyId}")]
        public async Task<IActionResult> DeleteCompany(long companyId)
        {
            await _adminService.DeleteCompanyAsync(companyId);
            return NoContent();
        }

        [HttpGet("companies/{companyId}")]
        public async Task<IActionResult> GetCompanyById(long companyId)
        {
            var company = await _adminService.GetCompanyByIdAsync(companyId);
            return Ok(company);
        }

        [HttpGet("companies")]
        public async Task<IActionResult> GetAllCompanies()
        {
            var companies = await _adminService.GetAllCompaniesAsync();
            return Ok(companies);
        }
    }
}
