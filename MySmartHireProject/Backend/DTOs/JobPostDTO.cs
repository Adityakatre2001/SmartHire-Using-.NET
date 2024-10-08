using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SmartHire.DTOs
{
    public class JobPostDTO
    {
        [JsonPropertyName("jobId")]
        public long? JobId { get; set; } 

        [Required]
        public DateTime PostDate { get; set; }

        [StringLength(5000)]
        public string JobDescription { get; set; }

        [Required]
        [JsonPropertyName("employerId")]
        public long EmployerId { get; set; } // Reference to Company by ID

        [Required]
        public double Salary { get; set; }

        [Required]
        [StringLength(100)]
        public string JobTitle { get; set; }

        [Required]
        [StringLength(255)]
        public string JobLocation { get; set; }

        public DateTime? CloseDate { get; set; } 
    }
}
