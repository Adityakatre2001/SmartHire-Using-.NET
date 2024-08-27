using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartHire.Models
{
    [Table("message")]
    public class Message
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long MessageId { get; set; }

        [Required]
        [ForeignKey("Sender")]
        public long SenderId { get; set; }

        public User Sender { get; set; }

        [Required]
        [ForeignKey("Receiver")]
        public long ReceiverId { get; set; }

        public User Receiver { get; set; }

        [Required]
        [StringLength(5000)]
        [Column("message_content", TypeName = "varchar(5000)")]
        public string MessageContent { get; set; }

        [Required]
        [Column("message_time")]
        public DateTime MessageTime { get; set; }
    }
}
