using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartHire.Models
{
    public class Profile
    {
        [Key]
        public long ProfileId { get; set; }

        [Required]
        public long UserId { get; set; }

        // Navigation property to User
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        [Required]
        [MaxLength(50)]
        public string Contact { get; set; }

        public byte[] Resume { get; set; }

        // Assuming skills are stored as a comma-separated string
        public string Skills { get; set; }

        [MaxLength(5000)]
        public string Summary { get; set; }

        [MaxLength(5000)]
        public string Experience { get; set; }
    }
}
