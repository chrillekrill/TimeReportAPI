namespace TimeReportMvc.Models.TimeReportModels;

public class TimeReportIndexModel
{
    public List<TimeReportModel> TimeReports { get; set; }

    public class TimeReportModel
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public int Minutes { get; set; }

        public string Description { get; set; }
    }
}