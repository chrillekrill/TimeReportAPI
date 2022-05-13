using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TimeReportApi.Data;
using TimeReportApi.DTO.ProjectDTOs;
using TimeReportApi.DTO.TimeReportDTOs;

namespace TimeReportApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TimeReportController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public TimeReportController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return Ok(_mapper.Map<List<TimeReportDto>>(_context.TimeReports));
        }
        [HttpPost]
        [Route("create")]
        public IActionResult Create(CreateTimeReportDto timeReport)
        {
            var newTimeReport = _mapper.Map<TimeReport>(timeReport);

            newTimeReport.Id = Guid.NewGuid();

            _context.TimeReports.Add(newTimeReport);

            _context.SaveChanges();

            var timeReportDto = _mapper.Map<TimeReportDto>(newTimeReport);

            return CreatedAtAction(nameof(GetOne), new { id = timeReportDto.Id }, timeReportDto);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetOne(string id)
        {
            Guid guidId;
            try
            {
                guidId = Guid.Parse(id);
            }
            catch
            {
                return NotFound("No time report found");
            }

            var timeReport = _context.TimeReports.FirstOrDefault(rep => rep.Id == guidId);
            if (timeReport == null) return NotFound("No time report found");

            var timeReportToReturn = _mapper.Map<TimeReportDto>(timeReport);

            return Ok(timeReportToReturn);
        }
    }
}
