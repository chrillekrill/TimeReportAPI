using Microsoft.AspNetCore.Mvc;

namespace TimeReportApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectController : Controller
    {
        public IActionResult Index()
        {
            return Ok();
        }
    }
}
