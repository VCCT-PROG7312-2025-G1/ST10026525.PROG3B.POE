using Microsoft.AspNetCore.Mvc;

namespace ST10026525.PROG3B.POE.Controllers
{
    public class ReportController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ReportForm()
        {
            // This will look for Views/Report/ReportForm.cshtml by default
            return View();
        }
    }
}
