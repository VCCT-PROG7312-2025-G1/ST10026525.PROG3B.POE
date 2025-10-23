using ST10026525.PROG3B.POE.Models;

namespace ST10026525.PROG3B.POE.Data
{
    public class UserSearchTracker
    {
        // Track category and date searches
        private Dictionary<string, int> categoryFrequency = new();
        private Dictionary<DateTime, int> dateFrequency = new();

        public void RecordSearch(string category, DateTime? date)
        {
            if (!string.IsNullOrEmpty(category))
            {
                if (categoryFrequency.ContainsKey(category))
                    categoryFrequency[category]++;
                else
                    categoryFrequency[category] = 1;
            }

            if (date.HasValue)
            {
                if (dateFrequency.ContainsKey(date.Value.Date))
                    dateFrequency[date.Value.Date]++;
                else
                    dateFrequency[date.Value.Date] = 1;
            }
        }

        // Get top N categories searched
        public List<string> GetTopCategories(int n = 3)
        {
            return categoryFrequency.OrderByDescending(kvp => kvp.Value)
                                    .Take(n)
                                    .Select(kvp => kvp.Key)
                                    .ToList();
        }

        // Get top N dates searched
        public List<DateTime> GetTopDates(int n = 3)
        {
            return dateFrequency.OrderByDescending(kvp => kvp.Value)
                                .Take(n)
                                .Select(kvp => kvp.Key)
                                .ToList();
        }

        // Recommend events based on top categories/dates
        public List<Event> RecommendEvents(List<Event> allEvents, int limit = 5)
        {
            var topCategories = GetTopCategories();
            var topDates = GetTopDates();

            var recommendations = allEvents
                .Where(e => topCategories.Contains(e.Category) || topDates.Contains(e.Date.Date))
                .OrderByDescending(e => topCategories.Contains(e.Category) ? 2 : 1) // prioritize category
                .Take(limit)
                .ToList();

            return recommendations;
        }
    }

}
