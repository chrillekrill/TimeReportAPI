namespace TimeReportMvc.Models.ProjectModels;

public class ProjectIndexModel
{
    public List<ProjectModel> Projects { get; set; } = new List<ProjectModel>();

    public class ProjectModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}