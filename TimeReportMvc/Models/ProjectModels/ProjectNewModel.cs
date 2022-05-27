using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using TimeReportMvc.Models.CustomerModels;


namespace TimeReportMvc.Models.ProjectModels;

public class ProjectNewModel
{
    public string Name { get; set; }
    [Display(Name = "Customer")]
    [Required(ErrorMessage = "{0} is required.")]
    public Guid CustomerId { get; set; }
    public List<SelectListItem> AllCustomers { get; set; } = new();
}