using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SmartHire.DTOs
{
    public class AuthDTO
    {
        [Required]
        [EmailAddress]
        [JsonPropertyName("email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)] //  Password should be between 6 and 100 characters
        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}
