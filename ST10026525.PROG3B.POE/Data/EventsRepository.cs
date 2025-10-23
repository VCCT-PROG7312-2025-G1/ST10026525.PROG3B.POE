using System;
using Microsoft.Data.SqlClient;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;
using ST10026525.PROG3B.POE.Models;

namespace ST10026525.PROG3B.POE.Data
{
    public static class EventsRepository
    {
        // === ADVANCED DATA STRUCTURES ===
        private static Stack<Event> _eventStack = new Stack<Event>();                  // For adding new events
        private static Queue<Event> _processingQueue = new Queue<Event>();             // For processing events
        private static Dictionary<string, Event> _eventDictionary = new Dictionary<string, Event>(StringComparer.OrdinalIgnoreCase); // For searching
        private static HashSet<string> _categories = new HashSet<string>(StringComparer.OrdinalIgnoreCase); // For unique categories
        private static List<Event> _eventList = new List<Event>();                     // Final display list

        // === STATIC CONSTRUCTOR ===
        static EventsRepository()
        {
            // Load once, only if data list is empty
            if (_eventList.Count == 0)
                LoadEventsFromDatabase();

            if (_eventList.Count == 0)
                SeedSampleData(); // fallback if DB empty

            ProcessEvents();
        }

        // === LOAD EVENTS FROM DATABASE ===
        private static void LoadEventsFromDatabase()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            string connectionString = config.GetConnectionString("EventsDB");

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT Id, Title, Description, Category, [Date], Location FROM Events";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var ev = new Event
                        {
                            Id = reader.GetGuid(0),
                            Title = reader.GetString(1),
                            Description = reader.GetString(2),
                            Category = reader.GetString(3),
                            Date = reader.GetDateTime(4),
                            Location = reader.GetString(5)
                        };

                        AddEvent(ev);
                    }
                }
            }

            ProcessEvents();
        }

        // === ADD EVENT (STACK USAGE) ===
        public static void AddEvent(Event e)
        {
            _eventStack.Push(e);
            _categories.Add(e.Category);
            _eventDictionary[e.Title] = e;
        }

        // === PROCESS EVENTS (QUEUE USAGE) ===
        public static void ProcessEvents()
        {
            while (_eventStack.Count > 0)
            {
                var nextEvent = _eventStack.Pop();
                _processingQueue.Enqueue(nextEvent);
            }

            while (_processingQueue.Count > 0)
            {
                var processedEvent = _processingQueue.Dequeue();
                _eventList.Add(processedEvent);
            }
        }

        // === SEARCH BY TITLE (DICTIONARY USAGE) ===
        public static Event SearchByTitle(string title)
        {
            if (_eventDictionary.ContainsKey(title))
                return _eventDictionary[title];
            return null;
        }

        // === GET UNIQUE CATEGORIES (SET USAGE) ===
        public static IEnumerable<string> GetCategories()
        {
            return _categories.OrderBy(c => c);
        }

        // === GET ALL EVENTS (LIST USAGE) ===
        public static List<Event> GetAllEvents()
        {
            return _eventList.OrderBy(e => e.Date).ToList();
        }

        // === SEED SAMPLE DATA (FALLBACK) ===
        private static void SeedSampleData()
        {
            AddEvent(new Event { Id = Guid.NewGuid(), Title = "Community Cleanup", Description = "Help clean up the park.", Category = "Community", Date = DateTime.Today.AddDays(2), Location = "Greenfield Park" });
            AddEvent(new Event { Id = Guid.NewGuid(), Title = "Tech Expo", Description = "Explore new tech.", Category = "Technology", Date = DateTime.Today.AddDays(7), Location = "Innovation Hub" });
            AddEvent(new Event { Id = Guid.NewGuid(), Title = "Farmers Market", Description = "Local produce and crafts.", Category = "Market", Date = DateTime.Today.AddDays(1), Location = "Town Square" });
            AddEvent(new Event { Id = Guid.NewGuid(), Title = "Marathon", Description = "Annual city run.", Category = "Sports", Date = DateTime.Today.AddDays(10), Location = "City Center" });
            AddEvent(new Event { Id = Guid.NewGuid(), Title = "Health Drive", Description = "Free checkups.", Category = "Health", Date = DateTime.Today.AddDays(4), Location = "Clinic Hall" });
            AddEvent(new Event { Id = Guid.NewGuid(), Title = "Music Festival", Description = "Live music and fun.", Category = "Entertainment", Date = DateTime.Today.AddDays(12), Location = "Downtown Plaza" });
            AddEvent(new Event { Id = Guid.NewGuid(), Title = "Coding Bootcamp", Description = "Learn C# in 2 days.", Category = "Education", Date = DateTime.Today.AddDays(5), Location = "IT Campus" });
            AddEvent(new Event { Id = Guid.NewGuid(), Title = "Charity Run", Description = "Support local charities.", Category = "Charity", Date = DateTime.Today.AddDays(3), Location = "Main Street" });
            AddEvent(new Event { Id = Guid.NewGuid(), Title = "Art Exhibition", Description = "Local artists showcase.", Category = "Art", Date = DateTime.Today.AddDays(15), Location = "Gallery One" });
            AddEvent(new Event { Id = Guid.NewGuid(), Title = "Food Fair", Description = "Tasting event.", Category = "Food", Date = DateTime.Today.AddDays(6), Location = "City Hall" });
            AddEvent(new Event { Id = Guid.NewGuid(), Title = "Career Expo", Description = "Meet employers.", Category = "Education", Date = DateTime.Today.AddDays(8), Location = "University Hall" });
            AddEvent(new Event { Id = Guid.NewGuid(), Title = "Pet Adoption Day", Description = "Adopt pets!", Category = "Community", Date = DateTime.Today.AddDays(9), Location = "Animal Shelter" });
            AddEvent(new Event { Id = Guid.NewGuid(), Title = "Book Fair", Description = "Discover new authors.", Category = "Literature", Date = DateTime.Today.AddDays(13), Location = "Library Hall" });
            AddEvent(new Event { Id = Guid.NewGuid(), Title = "Jazz Night", Description = "Evening of smooth jazz.", Category = "Music", Date = DateTime.Today.AddDays(11), Location = "The Lounge" });
            AddEvent(new Event { Id = Guid.NewGuid(), Title = "Startup Pitch", Description = "Entrepreneur competition.", Category = "Business", Date = DateTime.Today.AddDays(14), Location = "Innovation Hub" });

            ProcessEvents();
        }
        public static void DeleteEvent(Event e)
        {
            if (e == null) return;

            // Remove from stack if it's there
            if (_eventStack.Contains(e))
            {
                var tempStack = new Stack<Event>();
                while (_eventStack.Count > 0)
                {
                    var ev = _eventStack.Pop();
                    if (ev != e) tempStack.Push(ev);
                }
                while (tempStack.Count > 0) _eventStack.Push(tempStack.Pop());
            }

            // Remove from processing queue if it's there
            if (_processingQueue.Contains(e))
            {
                var tempQueue = new Queue<Event>();
                while (_processingQueue.Count > 0)
                {
                    var ev = _processingQueue.Dequeue();
                    if (ev != e) tempQueue.Enqueue(ev);
                }
                while (tempQueue.Count > 0) _processingQueue.Enqueue(tempQueue.Dequeue());
            }

            // Remove from dictionary
            if (_eventDictionary.ContainsKey(e.Title))
                _eventDictionary.Remove(e.Title);

            // Remove from list
            _eventList.Remove(e);

            // Optionally remove category if no other event uses it
            if (!_eventList.Any(ev => ev.Category == e.Category) &&
                !_eventStack.Any(ev => ev.Category == e.Category) &&
                !_processingQueue.Any(ev => ev.Category == e.Category))
            {
                _categories.Remove(e.Category);
            }
        }

    }
}
