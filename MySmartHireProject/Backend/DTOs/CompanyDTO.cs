using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SmartHire.DTOs
{
    public class CompanyDTO
    {
        [JsonPropertyName("companyId")]
        public long CompanyId { get; set; }

        [Required]
        [StringLength(100)]
        [JsonPropertyName("companyName")]
        public string CompanyName { get; set; }

        [Required]
        [StringLength(50)]
        [JsonPropertyName("industry")]
        public string Industry { get; set; }

        [Required]
        [StringLength(255)]
        [JsonPropertyName("location")]
        public string Location { get; set; }

        [StringLength(5000)]
        [JsonPropertyName("description")]
        public string Description { get; set; }
    }
}
