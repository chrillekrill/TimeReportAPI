using System.ComponentModel.DataAnnotations;

namespace TimeReportMvc.Data;

public class Customer
{
    public Guid Id { get; set; }
    [MaxLength(100)]
    public string Name { get; set; }
    public List<Project> Projects { get; set; } = new List<Project>();
    public List<TimeReport> TimeReports { get; set; } = new List<TimeReport>();
}