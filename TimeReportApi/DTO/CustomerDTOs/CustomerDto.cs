using TimeReportApi.DTO.ProjectDTOs;
using TimeReportApi.DTO.TimeReportDTOs;

namespace TimeReportApi.DTO.CustomerDTOs;
public class CustomerDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<ProjectDto> Projects { get; set; } = new List<ProjectDto>();
    public List<TimeReportDto> TimeReports { get; set; } = new List<TimeReportDto>();
}