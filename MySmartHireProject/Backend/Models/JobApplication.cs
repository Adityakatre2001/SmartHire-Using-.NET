using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartHire.Models
{
    [Table("application")]
    public class JobApplication
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ApplicationId { get; set; }

        [Required]
        [ForeignKey("User")]
        public long UserId { get; set; }
         
        public User User { get; set; }

        [Required]
        [ForeignKey("JobPosting")]
        public long JobPostingId { get; set; }

        public JobPosting JobPosting { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")] // Ensure the column type in DB is compatible
        public JobApplicationStatus Status { get; set; }

        [Required]
        [Column("application_date", TypeName = "date")]
        public DateTime ApplicationDate { get; set; }


    }
}
