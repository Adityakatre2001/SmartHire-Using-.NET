using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SmartHire.Models
{
    [Table("job_posting")]
    public class JobPosting
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long JobId { get; set; }

        [Required]
        [Column("post_date", TypeName = "date")]
        public DateTime PostDate { get; set; }

        [StringLength(5000)]
        [Column(TypeName = "varchar(5000)")]
        public string JobDescription { get; set; }

        [Required]
        [ForeignKey("Employer")]
        public long EmployerId { get; set; }

        public Company Employer { get; set; }

        [Required]
        public double Salary { get; set; }

        [Required]
        [StringLength(100)]
        public string JobTitle { get; set; }

        [Required]
        [StringLength(255)]
        public string JobLocation { get; set; }
        [Column("close_date", TypeName = "date")]
        public DateTime? CloseDate { get; set; }

        public ICollection<JobApplication> Applications { get; set; } = new HashSet<JobApplication>();
    }
}
