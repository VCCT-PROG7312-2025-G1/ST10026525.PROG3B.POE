using Microsoft.AspNetCore.Mvc;

namespace ST10026525.PROG3B.POE.Controllers
{
    public class MenuController : Controller
    {
        public IActionResult MainMenu()
        {
            return View();
        }
    }
}
