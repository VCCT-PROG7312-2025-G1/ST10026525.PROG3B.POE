using Microsoft.AspNetCore.Mvc;
using ST10026525.PROG3B.POE.Models;

namespace ST10026525.PROG3B.POE.Controllers
{
    public class ReportController : Controller
    {
        // Static repository for demo purposes
        private static ReportRepository _repository = new ReportRepository();

        public IActionResult ReportForm()
        {
            ViewBag.Categories = new string[] { "Sanitation", "Roads", "Utilities", "Electrivity", "Other" };
            return View();
        }

        [HttpPost]
        public IActionResult ReportForm(Report model)
        {
            ViewBag.Categories = new string[] { "Sanitation", "Roads", "Utilities", "Electrivity", "Other" };
            if (ModelState.IsValid)
            {
                _repository.Add(model);
                TempData["Success"] = "Report submitted successfully!";
                return RedirectToAction("ReportForm");
            }
            return View(model);
        }

        public IActionResult ViewReports(string searchLocation = "", string searchCategory = "")
        {
            IEnumerable<Report> reports = _repository.GetAll();

            if (!string.IsNullOrEmpty(searchLocation))
                reports = _repository.SearchByLocation(searchLocation);

            if (!string.IsNullOrEmpty(searchCategory))
                reports = _repository.SearchByCategory(searchCategory);

            return View(reports);
        }
    }
}
