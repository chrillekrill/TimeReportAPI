using TimeReportMvc.Models.ProjectModels;

namespace TimeReportMvc.Models.CustomerModels;

public class CustomerViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<ProjectIndexModel.ProjectModel> Projects { get; set; } = new List<ProjectIndexModel.ProjectModel>();
}