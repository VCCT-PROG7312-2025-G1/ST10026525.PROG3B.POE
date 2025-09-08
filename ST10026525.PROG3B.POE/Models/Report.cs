using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace ST10026525.PROG3B.POE.Models
{
    public class Report
    {
        [Required(ErrorMessage = "Location is required")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Please select a category")]
        public string Category { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

      //  public string Media { get; set; }

        public string? MediaFileName { get; set; } // We'll just store filename for now
    }
}
