using System.ComponentModel.DataAnnotations;

namespace TimeReportApi.Data;

public class TimeReport
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public int Minutes { get; set; }

    [MaxLength(200)] public string Description { get; set; }

    public Project Project { get; set; }
}