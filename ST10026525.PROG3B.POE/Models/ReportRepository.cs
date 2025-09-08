using Microsoft.AspNetCore.Mvc;

namespace ST10026525.PROG3B.POE.Models
{
    public static class ReportRepository
    {
        private static List<Report> _reports = new List<Report>();

        public static void AddReport(Report report)
        {
            _reports.Add(report);
        }

        public static List<Report> GetAllReports()
        {
            return _reports;
        }
    }
}
