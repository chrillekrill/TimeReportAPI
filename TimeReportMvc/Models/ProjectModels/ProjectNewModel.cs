using Microsoft.AspNetCore.Mvc.Rendering;

namespace TimeReportMvc.Models.ProjectModels;

public class ProjectNewModel
{
    public string Name { get; set; }
    public Guid CustomerId { get; set; }
    public List<SelectListItem> AllCustomers { get; set; } = new();
}