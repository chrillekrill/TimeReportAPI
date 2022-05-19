using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TimeReportMvc.Models.UserModel;

namespace TimeReportMvc.Controllers;
[Authorize(Roles = "Admin")]
public class UserController : Controller
{
    private readonly IConfiguration _configuration;

    public UserController(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    [HttpGet]
    public IActionResult Index()
    {
        var model = new UserNewModel();
        
        return View(model);
    }
    [HttpPost]
    public async Task<IActionResult> UserCreate(UserNewModel newUser)
    {
        if (ModelState.IsValid)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.PostAsJsonAsync(_configuration["urls:UserApi"], newUser);

                if (response.StatusCode != HttpStatusCode.OK) return NoContent();

                return RedirectToAction("Index", "Home");
            }
        }

        return NoContent();
    }
}