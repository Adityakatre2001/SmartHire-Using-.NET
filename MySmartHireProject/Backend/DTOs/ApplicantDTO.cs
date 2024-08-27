using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SmartHire.DTOs
{
    public class ApplicantDTO
    {
        [JsonPropertyName("userId")]
        public long UserId { get; set; }

        [Required]
        [StringLength(50)]
        [JsonPropertyName("username")]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        [JsonPropertyName("email")]
        public string Email { get; set; }

      
        // [Required]
        // [StringLength(20)]
        // [JsonPropertyName("role")]
        // public string Role { get; set; }
    }
}
