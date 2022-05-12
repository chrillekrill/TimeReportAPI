using System.ComponentModel.DataAnnotations;

namespace TimeReportApi.Data
{
    public class Customer
    {
        public Guid Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        public List<Project> Projects { get; set; }
        public List<TimeReport> timeReports { get; set; }
    }
}
