using System;

namespace ST10026525.PROG3B.POE.Models
{
    // Node to hold a single report
    public class ReportNode
    {
        public Report Data { get; set; }
        public ReportNode Next { get; set; }

        public ReportNode(Report data)
        {
            Data = data;
            Next = null;
        }
    }

    // Custom collection for Reports
    public class ReportRepository
    {
        private ReportNode head;

        public ReportRepository()
        {
            head = null;
        }

        // Add report
        public void Add(Report report)
        {
            var node = new ReportNode(report);
            if (head == null)
            {
                head = node;
                return;
            }

            var current = head;
            while (current.Next != null)
                current = current.Next;

            current.Next = node;
        }

        // Get all reports
        public IEnumerable<Report> GetAll()
        {
            var current = head;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }

        // Count reports
        public int Count()
        {
            int count = 0;
            var current = head;
            while (current != null)
            {
                count++;
                current = current.Next;
            }
            return count;
        }

        // Search by location
        public IEnumerable<Report> SearchByLocation(string location)
        {
            var current = head;
            while (current != null)
            {
                if (!string.IsNullOrEmpty(current.Data.Location) &&
                    current.Data.Location.Contains(location, StringComparison.OrdinalIgnoreCase))
                    yield return current.Data;

                current = current.Next;
            }
        }

        // Search by category
        public IEnumerable<Report> SearchByCategory(string category)
        {
            var current = head;
            while (current != null)
            {
                if (!string.IsNullOrEmpty(current.Data.Category) &&
                    current.Data.Category.Contains(category, StringComparison.OrdinalIgnoreCase))
                    yield return current.Data;

                current = current.Next;
            }
        }
    }
}
