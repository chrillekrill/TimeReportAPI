using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TimeReportApi.Data;
using TimeReportApi.DTO.ProjectDTOs;

namespace TimeReportApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ProjectController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public ProjectController(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    [HttpGet]
    public IActionResult Index()
    {
        return Ok(_mapper.Map<List<ProjectDto>>(_context.Projects.Include(p => p.TimeReports)));
    }

    [HttpPost]
    [Route("create")]
    public IActionResult Create(CreateProjectDto project)
    {
        var newProject = _mapper.Map<Project>(project);

        var customer = _context.Customers.FirstOrDefault(cus => cus.Id.ToString() == project.CustomerId);
        if (customer == null) return NotFound("Customer not found");

        customer.Projects.Add(newProject);

        _context.SaveChanges();

        var projectDto = _mapper.Map<ProjectDto>(newProject);

        return CreatedAtAction(nameof(GetOne), new { id = projectDto.Id }, projectDto);
    }
         
    [HttpGet]
    [Route("{id}")]
    public IActionResult GetOne(string id)
    {
        var project = _context.Projects.FirstOrDefault(proj => proj.Id.ToString() == id);
        if (project == null) return NotFound("No project found");

        var projectToReturn = _mapper.Map<ProjectDto>(project);

        return Ok(projectToReturn);
    }
        
    [HttpPut]
    [Route("edit/{id}")]
    public IActionResult EditProject(string id, EditProjectDto editedProject)
    {
        var project = _context.Projects.FirstOrDefault(proj => proj.Id.ToString() == id);
        if (project == null) return NotFound("No project found");
            
        _mapper.Map(editedProject, project);

        _context.SaveChanges();

        var projectToReturn = _mapper.Map<ProjectDto>(project);

        return Ok(projectToReturn);
    }
    [HttpDelete]
    [Route("delete/{id}")]
    public IActionResult DeleteProject(string id)
    {
        var project = _context.Projects.FirstOrDefault(proj => proj.Id.ToString() == id);

        if(project == null) return NotFound("No project found");

        _context.Projects.Remove(project);

        _context.SaveChanges();

        return Ok($"Project with the Id: {id} was deleted");
    }
}