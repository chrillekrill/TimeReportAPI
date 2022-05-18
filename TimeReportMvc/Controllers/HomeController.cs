using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TimeReportMvc.Models;

namespace TimeReportMvc.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        /*var user = new UserDto
        {
            Username = "Christoffer",
            Password = "Hejsan123#"
        };

        var url = "https://localhost:8080/user/login";

        HttpClient httpClient = new HttpClient();

        var response = await httpClient.PostAsJsonAsync(url, user);

        var responseResult = JsonConvert.DeserializeObject<UserJsonDto>(response.Content.ReadAsStringAsync().Result);

        Response.Cookies.Append("UserCookie", responseResult.Jwt, new CookieOptions{HttpOnly = true});*/

        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}