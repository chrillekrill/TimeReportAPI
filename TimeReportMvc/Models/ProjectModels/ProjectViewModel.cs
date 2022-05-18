using TimeReportMvc.Models.CustomerModels;

namespace TimeReportMvc.Models.ProjectModels;

public class ProjectViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid CustomerId { get; set; }

    public CustomerViewModel Customer { get; set; }
}