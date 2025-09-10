using Microsoft.AspNetCore.Mvc;
using ST10026525.PROG3B.POE.Models;
using ST10026525.PROG3B.POE.Data;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace ST10026525.PROG3B.POE.Controllers
{
    public class ReportController : Controller
    {
        // GET: Show form
        public IActionResult ReportForm()
        {
            ViewBag.Categories = new List<string>
    {
        "Sanitation",
        "Roads",
        "Utilities",
        "Electricity",
        "Other"
    };

            return View();
        }

        // POST: Submit report
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ReportForm(IFormFile Media, Report model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Handle media upload
            if (Media != null && Media.Length > 0)
            {
                var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                if (!Directory.Exists(uploadsPath))
                    Directory.CreateDirectory(uploadsPath);

                var filePath = Path.Combine(uploadsPath, Media.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    Media.CopyTo(stream);
                }
                model.MediaFileName = Media.FileName;
            }

            // Add report to queue
            ReportRepository.AddReport(model);

            TempData["Success"] = "Report submitted successfully!";
            return RedirectToAction("ReportForm");
        }

        // GET: View all reports
        public IActionResult ViewReports()
        {
            var reports = ReportRepository.GetAllReports();
            return View(reports);
        }
    }
}
