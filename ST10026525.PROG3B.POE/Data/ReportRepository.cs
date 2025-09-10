using ST10026525.PROG3B.POE.Models;
using System.Collections.Generic;

namespace ST10026525.PROG3B.POE.Data
{
    public static class ReportRepository
    {
        // Queue to store reports FIFO
        private static Queue<Report> _reports = new Queue<Report>();

        // Add a new report
        public static void AddReport(Report report)
        {
            _reports.Enqueue(report);
        }

        // Get all reports (for display)
        public static Queue<Report> GetAllReports()
        {
            return new Queue<Report>(_reports); // return a copy to avoid modification
        }

        // Get the next report to process
        public static Report DequeueReport()
        {
            if (_reports.Count > 0)
                return _reports.Dequeue();
            return null;
        }

        // Optional: get count
        public static int Count => _reports.Count;
    }
}
