using Microsoft.AspNetCore.Mvc;

namespace TimeReportApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
    }
}
