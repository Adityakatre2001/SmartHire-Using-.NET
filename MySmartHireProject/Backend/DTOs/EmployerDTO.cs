using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SmartHire.DTOs
{
    public class EmployerDTO
    {
        [JsonPropertyName("userId")]
        public long UserId { get; set; }

        [JsonPropertyName("username")]
        public string Username { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("companyName")]
        public string CompanyName { get; set; } // Relevant to employers

        [JsonPropertyName("role")]
        public string Role { get; set; } // ex employer
    }
}
