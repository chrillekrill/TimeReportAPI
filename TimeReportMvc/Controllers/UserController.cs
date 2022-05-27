using System.Diagnostics;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using TimeReportMvc.Models.UserModel;

namespace TimeReportMvc.Controllers;
[Authorize(Roles = "Admin")]
public class UserController : Controller
{
    private readonly IConfiguration _configuration;
    private readonly IToastNotification _toastNotification;

    public UserController(IConfiguration configuration, IToastNotification toastNotification)
    {
        _configuration = configuration;
        _toastNotification = toastNotification;
    }

    [HttpGet]
    public IActionResult UserCreate()
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

                var accessToken = Request.Cookies["UserCookie"];

                httpClient.BaseAddress = new Uri(_configuration["urls:UserApi"]);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

                var response = await httpClient.PostAsJsonAsync(_configuration["urls:UserApi"], new { username = newUser.Username, password = newUser.Password, role = newUser.Role });
                Debug.WriteLine(response.Content.ReadAsStringAsync().Result);
                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    _toastNotification.AddErrorToastMessage(response.Content.ReadAsStringAsync().Result);
                    
                    return View(newUser);
                }
                _toastNotification.AddSuccessToastMessage(response.Content.ReadAsStringAsync().Result);
                return RedirectToAction("Index", "Home");
            }
        }

        return NoContent();
    }
}