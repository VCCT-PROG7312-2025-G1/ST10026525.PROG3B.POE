using Microsoft.AspNetCore.Mvc;
using ST10026525.PROG3B.POE.Data;
using ST10026525.PROG3B.POE.Models;


namespace ST10026525.PROG3B.POE.Controllers
{
    public class EventsController : Controller
    {
        private readonly AppDbContext _context;
        private static readonly UserSearchTracker _searchTracker;

        public EventsController(AppDbContext context, UserSearchTracker searchTracker)
        {
            _context = context;
        }

        // === DISPLAY EVENTS ===
        [HttpGet]
        public IActionResult Index()
        {
            // 1️⃣ Load all events from the database
            var dbEvents = _context.Events.OrderBy(e => e.Date).ToList();

            // 2️⃣ Keep track of newly added events for processing
            var newEvents = new List<Event>();

            // 3️⃣ Sync database events with in-memory repository
            foreach (var e in dbEvents)
            {
                if (EventsRepository.SearchByTitle(e.Title) == null)
                {
                    EventsRepository.AddEvent(e);
                    newEvents.Add(e); // Only process new events
                }
            }

            // 4️⃣ Process only the newly added events
            if (newEvents.Count > 0)
            {
                EventsRepository.ProcessEvents();
            }

            // 5️⃣ Get all events from repository for recommendation logic
            var allEvents = EventsRepository.GetAllEvents();

            // 6️⃣ Get recommended events based on search history
            var recommendedEvents = _searchTracker.RecommendEvents(allEvents);
            ViewBag.Recommended = recommendedEvents;

            // 7️⃣ Get all categories for the filter dropdown
            ViewBag.Categories = EventsRepository.GetCategories();

            // 8️⃣ Pass database events to the view for display
            return View(dbEvents);

        }

        // === CREATE NEW EVENT (ADMIN ONLY) ===
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Event newEvent)
        {
            if (ModelState.IsValid)
            {
                newEvent.Id = Guid.NewGuid();

                // Add to both DB and in-memory structures
                _context.Events.Add(newEvent);
                _context.SaveChanges();

                EventsRepository.AddEvent(newEvent);
                EventsRepository.ProcessEvents();

                TempData["Success"] = "Event added successfully!";
                return RedirectToAction(nameof(Index));
            }

            return View(newEvent);
        }

        // === SEARCH EVENTS ===
        [HttpGet]
        public JsonResult Search(string q, string category, DateTime? from, DateTime? to)
        {
            // Record user search
            _searchTracker.RecordSearch(category, from);

            var results = EventsRepository.GetAllEvents().AsEnumerable();

            if (!string.IsNullOrEmpty(q))
                results = results.Where(e => e.Title.Contains(q, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrEmpty(category))
                results = results.Where(e => e.Category == category);

            if (from.HasValue)
                results = results.Where(e => e.Date >= from.Value);

            if (to.HasValue)
                results = results.Where(e => e.Date <= to.Value);

            return Json(results);
        }

        // === DELETE EVENT (ADMIN ONLY) ===
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Guid id)
        {
            var ev = _context.Events.FirstOrDefault(e => e.Id == id);
            if (ev == null)
            {
                TempData["Error"] = "Event not found!";
                return RedirectToAction(nameof(Index));
            }

            _context.Events.Remove(ev);
            _context.SaveChanges();

            TempData["Success"] = "Event deleted successfully!";
            return RedirectToAction(nameof(Index));
        }
    }
}

