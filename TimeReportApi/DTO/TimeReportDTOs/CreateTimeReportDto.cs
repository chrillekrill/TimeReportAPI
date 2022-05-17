namespace TimeReportApi.DTO.TimeReportDTOs;

public class CreateTimeReportDto
{
    public DateTime Date { get; set; }
    public int Minutes { get; set; }
    public string Description { get; set; }
    public Guid ProjectId { get; set; }
}