using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TimeReportApi.Data;
using TimeReportApi.DTO.ProjectDTOs;
using TimeReportApi.DTO.TimeReportDTOs;

namespace TimeReportApi.Controllers;

[ApiController]
[Authorize]
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
        return Ok(_mapper.Map<List<TimeReportDto>>(_context.TimeReports.Include(p => p.Project)));
    }

    [HttpPost]
    public IActionResult Create(CreateTimeReportDto timeReport)
    {
        var newTimeReport = _mapper.Map<TimeReport>(timeReport);

        //var customer = _context.Customers.Include(cus => cus.Projects).ThenInclude(proj => proj.TimeReports).First(c => c.Id == timeReport.CustomerId);

        var project = _context.Projects.FirstOrDefault(p => p.Id == timeReport.ProjectId);
        if (project == null) return NotFound("Project not found");

        project.TimeReports.Add(newTimeReport);

        //customer.Projects.First(p => p.Id == timeReport.ProjectId).TimeReports.Add(newTimeReport);

        _context.SaveChanges();

        var timeReportDto = _mapper.Map<TimeReportDto>(newTimeReport);

        return CreatedAtAction(nameof(GetOne), new { id = timeReportDto.Id }, timeReportDto);
    }

    [HttpGet]
    [Route("project/{id}")]
    public IActionResult GetAllProjectReports(string id)
    {
        var reports = new List<TimeReportDto>();

        var project = _context.Projects.Include(t => t.TimeReports).FirstOrDefault(p => p.Id.ToString() == id);

        if (project == null) return NotFound("Project not found");

        reports = _mapper.Map<List<TimeReportDto>>(project.TimeReports);

        return Ok(reports);
    }

    [HttpGet]
    [Route("{id}")]
    public IActionResult GetOne(string id)
    {
        var timeReport = _context.TimeReports.Include(p => p.Project).FirstOrDefault(rep => rep.Id.ToString() == id);
        if (timeReport == null) return NotFound("No time report found");

        var timeReportToReturn = _mapper.Map<TimeReportDto>(timeReport);

        return Ok(timeReportToReturn);
    }

    [HttpPut]
    [Route("{id}")]
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
    [Route("{id}")]
    public IActionResult DeleteTimeReport(string id)
    {
        var timeReport = _context.TimeReports.FirstOrDefault(t => t.Id.ToString() == id);

        if (timeReport == null) return NotFound("No time report found");

        _context.TimeReports.Remove(timeReport);

        _context.SaveChanges();

        return Ok($"Time report with the Id: {id} was deleted");
    }
}