using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SmartHire.DTOs;
using SmartHire.Services;

namespace SmartHire.Controllers
{
    [Route("users")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
   // [EnableCors("AllowAll")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IApplicationService _applicationService;

        public UserController(IUserService userService, IApplicationService applicationService)
        {
            _userService = userService;
            _applicationService = applicationService;
        }

       /* // User SignIn
        [HttpPost("signin")]
        public IActionResult UserSignIn([FromBody] AuthDTO dto)
        {
            try
            {
                var respDto = _userService.AuthenticateUser(dto);
                return Ok(respDto);
            }
            catch (Exception e)
            {
                return NotFound(new ApiResponse(e.Message));
            }
        }*/

        // Application Management
        [HttpPost("applications")]
        public IActionResult CreateApplication([FromBody] ApplicantDTO applicationDTO)
        {
            var createdApplication = _applicationService.CreateApplicationAsync(applicationDTO);
            return Ok(createdApplication);
        }

        [HttpPut("applications/{applicationId}")]
        public IActionResult UpdateApplication([FromRoute] long applicationId, [FromBody] ApplicantDTO applicationDTO)
        {
            var updatedApplication = _applicationService.UpdateApplicationAsync(applicationId, applicationDTO);
            return Ok(updatedApplication);
        }

        [HttpDelete("applications/{applicationId}")]
        public IActionResult DeleteApplication([FromRoute] long applicationId)
        {
            _applicationService.DeleteApplicationAsync(applicationId);
            return NoContent();
        }

        [HttpGet("applications/{applicationId}")]
        public IActionResult GetApplicationById([FromRoute] long applicationId)
        {
            var applicationDTO = _applicationService.GetApplicationByIdAsync(applicationId);
            return Ok(applicationDTO);
        }

        [HttpGet("applications")]
        public IActionResult GetAllApplications()
        {
            var applications = _applicationService.GetAllApplicationsAsync();
            return Ok(applications);
        }
    }
}
