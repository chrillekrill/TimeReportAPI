namespace TimeReportApi.DTO.TimeReportDTOs
{
    public class TimeReportDTO
    {
        public Guid Id { get; set; }
        public DateTime date { get; set; }
        public int Minutes { get; set; }
        public string Description { get; set; }
    }
}
