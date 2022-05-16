using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TimeReportMvc.Data;
using TimeReportMvc.Models.CustomerModels;

namespace TimeReportMvc.Controllers;

[Authorize(Roles="Admin")]
public class CustomerController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    public CustomerController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }
    // GET
    public IActionResult Index()
    {
        var model = new CustomerIndexModel();
        
        string apiUrl = "Https://localhost:8080/customer";

        //var httpClient = new HttpClient();

        using (var httpClient = new HttpClient())
        {
            var accessToken = Request.Cookies["UserCookie"];
            
            httpClient.BaseAddress = new Uri(apiUrl);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

            var data = httpClient.GetStringAsync(apiUrl).Result;
            
            var newModel = JsonConvert.DeserializeObject<List<CustomerModel>>(data);
            
            model.Customers = newModel.Select(e => new CustomerModel
            {
                Name = e.Name
            }).ToList();
        }
        
        return View(model);
    }
}