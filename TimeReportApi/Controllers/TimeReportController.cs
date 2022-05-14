using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TimeReportApi.Data;
using TimeReportApi.DTO.ProjectDTOs;
using TimeReportApi.DTO.TimeReportDTOs;

namespace TimeReportApi.Controllers;

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

        var customer = _context.Customers.Include(cus => cus.Projects).ThenInclude(proj => proj.TimeReports).First(c => c.Id.ToString() == timeReport.CustomerId);

        //var project = _context.Projects.FirstOrDefault(p => p.Id.ToString() == timeReport.ProjectId);
        //if (project == null) return NotFound("Project not found");

        //project.TimeReports.Add(newTimeReport);
            
        customer.Projects.First(p => p.Id.ToString() == timeReport.ProjectId).TimeReports.Add(newTimeReport);
        customer.TimeReports.Add(newTimeReport);

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
    
    [HttpPut]
    [Route("edit/{id}")]
    public IActionResult EditTimeReport(string id, EditTimeReportDto editedTimeReport)
    {
        var timeReport = _context.TimeReports.FirstOrDefault(t => t.Id.ToString() == id);
        if (timeReport == null) return NotFound("No time report found");
            
        _mapper.Map(editedTimeReport, timeReport);

        _context.SaveChanges();

        var timeReportToReturn = _mapper.Map<ProjectDto>(timeReport);

        return Ok(timeReportToReturn);
    }
    
    [HttpDelete]
    [Route("delete/{id}")]
    public IActionResult DeleteTimeReport(string id)
    {
        var timeReport = _context.TimeReports.FirstOrDefault(t => t.Id.ToString() == id);

        if(timeReport == null) return NotFound("No time report found");

        _context.TimeReports.Remove(timeReport);

        _context.SaveChanges();

        return Ok($"Time report with the Id: {id} was deleted");
    }
}