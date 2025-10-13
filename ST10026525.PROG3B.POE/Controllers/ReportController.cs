using Microsoft.AspNetCore.Mvc;
using ST10026525.PROG3B.POE.Models;
using ST10026525.PROG3B.POE.Data;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace ST10026525.PROG3B.POE.Controllers
{
    public class ReportController : Controller
    {
        private List<string> GetCategories() => new List<string>
        {
            "Sanitation",
            "Roads",
            "Utilities",
            "Electricity",
            "Other"
        };

        // GET: Show form
        public IActionResult ReportForm()
        {
            ViewBag.Categories = GetCategories();
            return View();
        }

        // POST: Submit report
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ReportForm(IFormFile MediaFileName, Report model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = GetCategories(); // ✅ repopulate
                return View(model);
            }

            // ✅ Handle media upload
            if (MediaFileName != null && MediaFileName.Length > 0)
            {
                var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                if (!Directory.Exists(uploadsPath))
                    Directory.CreateDirectory(uploadsPath);

                var filePath = Path.Combine(uploadsPath, MediaFileName.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    MediaFileName.CopyTo(stream);
                }
                model.MediaFileName = MediaFileName.FileName;
            }

            // Save report
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
