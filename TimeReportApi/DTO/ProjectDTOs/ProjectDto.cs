using TimeReportApi.DTO.TimeReportDTOs;

namespace TimeReportApi.DTO.ProjectDTOs;
public class ProjectDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<TimeReportDto> TimeReports { get; set; } = new List<TimeReportDto>();
    }
