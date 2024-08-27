using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SmartHire.DTOs
{
    public class JobApplicationDTO
    {
        [JsonPropertyName("applicationId")]
        public long? ApplicationId { get; set; } // can be Nullable so used ?

        [Required]
        [JsonPropertyName("userId")]
        public long UserId { get; set; } // Reference to User by ID

        [Required]
        [JsonPropertyName("jobId")]
        public long JobId { get; set; } // Reference to JobPosting by ID

        [Required]
        [StringLength(50)]
        public string Status { get; set; }

        [Required]
        public DateTime ApplicationDate { get; set; }
    }
}

