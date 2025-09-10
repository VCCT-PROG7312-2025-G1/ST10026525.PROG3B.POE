using Microsoft.AspNetCore.Mvc;

namespace ST10026525.PROG3B.POE.Models
{
    public class Report
    {
        public int Id { get; set; } // Auto-increment if using DB
        public string Location { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string MediaFileName { get; set; } // Optional image file
        public DateTime SubmittedAt { get; set; } = DateTime.Now; // Timestamp for queue order
    }
}
