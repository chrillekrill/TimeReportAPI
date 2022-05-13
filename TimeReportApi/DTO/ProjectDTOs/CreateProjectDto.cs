using System.ComponentModel.DataAnnotations.Schema;
using TimeReportApi.DTO.CustomerDTOs;

namespace TimeReportApi.DTO.ProjectDTOs;
public class CreateProjectDto
{
        public string Name { get; set; }
        public string CustomerId { get; set; }
}