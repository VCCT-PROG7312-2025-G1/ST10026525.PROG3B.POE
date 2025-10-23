using Microsoft.AspNetCore.Mvc;
using ST10026525.PROG3B.POE.Data;
using ST10026525.PROG3B.POE.Models;

public class AdminController : Controller
{
    private readonly AppDbContext _context;

    public AdminController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(string username, string password)
    {
        var admin = _context.Admins
            .FirstOrDefault(a => a.Username == username && a.Password == password);

        if (admin != null)
        {
            // Set session
            HttpContext.Session.SetString("AdminUsername", admin.Username);
            return RedirectToAction("Dashboard");
        }

        ViewBag.Error = "Invalid username or password.";
        return View();
    }

    [HttpGet]
    public IActionResult Dashboard()
    {
        var session = HttpContext.Session.GetString("AdminUsername");
        if (string.IsNullOrEmpty(session))
        {
            return RedirectToAction("Login");
        }

        var events = _context.Events.OrderBy(e => e.Date).ToList();
        return View(events);
    }

    [HttpGet]
    public IActionResult Logout()
    {
        HttpContext.Session.Remove("AdminUsername");
        return RedirectToAction("Login");
    }

    // Add/Delete Event actions
    [HttpGet]
    public IActionResult CreateEvent()
    {
        return View();
    }

    [HttpPost]
    public IActionResult CreateEvent(Event e)
    {
        if (ModelState.IsValid)
        {
            _context.Events.Add(e);
            _context.SaveChanges();
            return RedirectToAction("Dashboard");
        }
        return View(e);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteEvent(Guid id)
    {
        // Find event in database
        var e = _context.Events.Find(id);
        if (e != null)
        {
            _context.Events.Remove(e);
            _context.SaveChanges();

            // Also remove from repository
            var repoEv = EventsRepository.SearchByTitle(e.Title);
            if (repoEv != null)
                EventsRepository.DeleteEvent(repoEv);
        }

        return RedirectToAction("Dashboard");
    }

}
