using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeReportApi.Data
{
    public class TimeReport
    {
        public Guid Id { get; set; }
        public DateTime date { get; set; }
        public int Minutes { get; set; }
        [MaxLength(200)]
        public string Description { get; set; }

    }
}
