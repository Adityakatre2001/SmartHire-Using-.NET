using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SmartHire.DTOs
{
    public class ProfileDTO
    {
        [JsonPropertyName("profileId")]
        public long ProfileId { get; set; }

        [Required]
        [JsonPropertyName("userId")]
        public long UserId { get; set; } // Reference to UserDTO

        [Required]
        [StringLength(50)]
        public string Contact { get; set; }

        public byte[] Resume { get; set; }

        public List<string> Skills { get; set; }

        [StringLength(5000)]
        public string Summary { get; set; }

        [StringLength(5000)]
        public string Experience { get; set; }
    }
}
