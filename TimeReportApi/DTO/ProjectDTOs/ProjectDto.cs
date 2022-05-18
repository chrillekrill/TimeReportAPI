namespace TimeReportApi.DTO.ProjectDTOs;

public class ProjectDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid CustomerId { get; set; }
}