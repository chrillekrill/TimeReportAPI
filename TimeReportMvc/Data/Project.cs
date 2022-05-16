using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeReportMvc.Data;
public class Project
{
    public Guid Id { get; set; }
    [MaxLength(100)]
    public string Name { get; set; }
    public List<TimeReport> TimeReports { get; set; } = new List<TimeReport>();
}
