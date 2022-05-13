using TimeReportApi.DTO.TimeReportDTOs;

namespace TimeReportApi.DTO.ProjectDTOs
{
    public class ProjectDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<TimeReportDTO> timeReports { get; set; }
        public List<CustomerDTO> Customers { get; set; }
    }
}
