using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SmartHire.DTOs
{
    public class MessageDTO
    {
        [JsonPropertyName("messageId")]
        public long MessageId { get; set; }

        [Required]
        [JsonPropertyName("senderId")]
        public long SenderId { get; set; } // Reference to UserDTO

        [Required]
        [JsonPropertyName("receiverId")]
        public long ReceiverId { get; set; } // Reference to UserDTO

        [Required]
        [StringLength(5000)]
        public string MessageContent { get; set; }

        [Required]
        public DateTime MessageTime { get; set; }
    }
}
