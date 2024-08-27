using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SmartHire.DTOs;
using SmartHire.Services;

namespace SmartHire.Controllers
{
    [Route("employers")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
   // [EnableCors("AllowAll")]
    public class EmployerController : ControllerBase
    {
        private readonly IEmployerService _employerService;

        public EmployerController(IEmployerService employerService)
        {
            _employerService = employerService;
        }

        // Job Posting Management
        [HttpPost("jobs")]
        public IActionResult CreateJobPosting([FromBody] JobPostDTO jobPostingDTO)
        {
            var createdJobPosting = _employerService.CreateJobPosting(jobPostingDTO);
            return Ok(createdJobPosting);
        }

        [HttpPut("jobs/{jobId}")]
        public IActionResult UpdateJobPosting([FromRoute] long jobId, [FromBody] JobPostDTO jobPostingDTO)
        {
            var updatedJobPosting = _employerService.UpdateJobPosting(jobId, jobPostingDTO);
            return Ok(updatedJobPosting);
        }

        [HttpDelete("jobs/{jobId}")]
        public IActionResult DeleteJobPosting([FromRoute] long jobId)
        {
            _employerService.DeleteJobPosting(jobId);
            return NoContent();
        }

        [HttpGet("jobs/{jobId}")]
        public IActionResult GetJobPostingById([FromRoute] long jobId)
        {
            var jobPostingDTO = _employerService.GetJobPostingById(jobId);
            return Ok(jobPostingDTO);
        }

        [HttpGet("jobs")]
        public IActionResult GetAllJobPostings()
        {
            var jobPostings = _employerService.GetAllJobPostings();
            return Ok(jobPostings);
        }

        // Application Management
        [HttpGet("jobs/{jobId}/applications")]
        public IActionResult GetApplicationsForJob([FromRoute] long jobId)
        {
            var applications = _employerService.GetApplicationsForJob(jobId);
            return Ok(applications);
        }
    }
}
