using Microsoft.AspNetCore.Mvc;

namespace TimeReportMvc.Controllers;

public class ProjectController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}