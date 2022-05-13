using TimeReportApi.DTO.ProjectDTOs;
using TimeReportApi.DTO.TimeReportDTOs;

namespace TimeReportApi.DTO
{
    public class CustomerDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<ProjectDTO> Projects { get; set; }
        public List<TimeReportDTO> Reports { get; set; }
    }
}
