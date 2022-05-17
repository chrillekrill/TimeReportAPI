using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeReportApi.Data;
public class Project
{
    public Guid Id { get; set; }
    [MaxLength(100)]
    public string Name { get; set; }
    public List<TimeReport> TimeReports { get; set; } = new List<TimeReport>();
    public Customer customer { get; set; }
}
