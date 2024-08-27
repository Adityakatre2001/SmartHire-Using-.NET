using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartHire.Models
{
    [Table("company")]
    public class Company
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long CompanyId { get; set; }

        [Required]
        [StringLength(100)]
        [Column("company_name")]
        public string CompanyName { get; set; }

        [Required]
        [StringLength(50)]
        public string Industry { get; set; }

        [Required]
        [StringLength(255)]
        public string Location { get; set; }

        [StringLength(5000)]
        public string Description { get; set; }

        // Navigation property
        public ICollection<JobPosting> JobPostings { get; set; } = new List<JobPosting>();
    }
}
