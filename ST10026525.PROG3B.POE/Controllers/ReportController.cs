using Microsoft.AspNetCore.Mvc;
using ST10026525.PROG3B.POE.Models;

namespace ST10026525.PROG3B.POE.Controllers
{
    public class ReportController : Controller
    {
        [HttpGet]
        public IActionResult ReportForm()
        {
            ViewBag.Categories = new List<string> { "Sanitation", "Roads", "Utilities", "Electricity", "Other" };
            return View();
        }
        
        [HttpPost]
        public IActionResult ReportForm(Report model, IFormFile? Media)
        {
            if (ModelState.IsValid)
            {
                if (Media != null && Media.Length > 0)
                {
                    var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                    if (!Directory.Exists(uploadsPath))
                        Directory.CreateDirectory(uploadsPath);

                    var fileName = Path.GetFileName(Media.FileName);
                    var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(fileName);
                    var filePath = Path.Combine(uploadsPath, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        Media.CopyTo(stream);
                    }

                    model.MediaFileName = uniqueFileName; // store unique file name
                }

                // Save to list
                ReportRepository.AddReport(model);

                TempData["Success"] = "Your report has been submitted!";
                return RedirectToAction("ReportForm");
            }

            ViewBag.Categories = new List<string> { "Sanitation", "Roads", "Utilities", "Electricity", "Other" };
            return View(model);
        }
        [HttpGet]
        public IActionResult ViewReports()
        {
            var reports = ReportRepository.GetAllReports();
            return View(reports);
        }
    }
}
