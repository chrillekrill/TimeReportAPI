namespace TimeReportApi.DTO.TimeReportDTOs;
public class TimeReportDto
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public int Minutes { get; set; }
        public string Description { get; set; }
    }
