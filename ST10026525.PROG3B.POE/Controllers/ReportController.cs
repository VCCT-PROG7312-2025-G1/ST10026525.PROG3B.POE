using Microsoft.AspNetCore.Mvc;
using ST10026525.PROG3B.POE.Models;

namespace ST10026525.PROG3B.POE.Controllers
{
    public class ReportController : Controller
    {
        [HttpGet]
        public IActionResult ReportForm()
        {
            ViewBag.Categories = new List<string> { "Sanitation", "Roads", "Utilities", "Other" };
            return View();
        }
        
        [HttpPost]
        public IActionResult ReportForm(Report model, IFormFile? Media)
        {
            if (ModelState.IsValid)
            {
                // Handle media upload (optional)
                if (Media != null && Media.Length > 0)
                {
                    var filePath = Path.Combine("wwwroot/uploads", Media.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        Media.CopyTo(stream);
                    }
                    model.MediaFileName = Media.FileName;
                }
                // Save to list
                ReportRepository.AddReport(model);

                TempData["Success"] = "Your report has been submitted!";
                return RedirectToAction("ReportForm");
            }

            ViewBag.Categories = new List<string> { "Sanitation", "Roads", "Utilities", "Other" };
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
