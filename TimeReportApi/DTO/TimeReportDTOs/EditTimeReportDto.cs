namespace TimeReportApi.DTO.TimeReportDTOs;

public class EditTimeReportDto
{
    public DateTime Date { get; set; }
    public int Minutes { get; set; }
    public string Description { get; set; }
}