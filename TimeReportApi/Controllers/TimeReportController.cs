using Microsoft.AspNetCore.Mvc;

namespace TimeReportApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TimeReportController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
