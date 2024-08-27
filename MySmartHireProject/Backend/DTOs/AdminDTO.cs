using System.ComponentModel.DataAnnotations;

namespace SmartHire.DTOs
{
    public class AdminDTO
    {
        [Required]
        public long UserId { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }
    }
}
