
using System;

namespace SmartHire.DTOs
{
    public class ApiResponse
    {
        public string Message { get; set; }
        public DateTime TimeStamp { get; set; }

        public ApiResponse(string message)
        {
            Message = message;
            TimeStamp = DateTime.Now;
        }

        // Parameterless constructor is required for deserialization
        public ApiResponse()
        {
            
        }

        public override string ToString()
        {
            return $"Message: {Message}, TimeStamp: {TimeStamp}";
        }
    }
}
