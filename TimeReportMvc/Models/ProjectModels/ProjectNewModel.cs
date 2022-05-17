using Microsoft.AspNetCore.Mvc.Rendering;
using TimeReportMvc.Models.CustomerModels;

namespace TimeReportMvc.Models.ProjectModels;

public class ProjectNewModel
{
    public string Name { get; set; }
    public Guid CustomerId { get; set; }
    public List<SelectListItem> AllCustomers { get; set; } = new List<SelectListItem>();

}