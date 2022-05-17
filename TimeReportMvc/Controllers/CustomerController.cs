using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Newtonsoft.Json;
using TimeReportMvc.Data;
using TimeReportMvc.Models.CustomerModels;

namespace TimeReportMvc.Controllers;

[Authorize(Roles="Admin")]
public class CustomerController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IConfiguration _configuration;

    public CustomerController(ApplicationDbContext context, UserManager<IdentityUser> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }
    // GET
    public IActionResult Index()
    {
        var model = new CustomerIndexModel();

        using (var httpClient = new HttpClient())
        {
            var accessToken = Request.Cookies["UserCookie"];
            
            httpClient.BaseAddress = new Uri(_configuration["urls:CustomerApi"]);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

            var data = httpClient.GetStringAsync(_configuration["urls:CustomerApi"]).Result;
            
            var newModel = JsonConvert.DeserializeObject<List<CustomerModel>>(data);
            
            model.Customers = newModel.Select(e => new CustomerModel
            {
                Name = e.Name,
                Id = e.Id
            }).ToList();
        }
        return View(model);
    }
    [HttpGet]
    public IActionResult CreateCustomer() //OnGet
    {
        var model = new CustomerNewModel();
        return View(model);
    }
    [HttpPost]
    public async Task<IActionResult> CreateCustomer(CustomerNewModel newCustomer)
    {
        if (ModelState.IsValid)
        {
            using (var httpClient = new HttpClient())
            {
                var accessToken = Request.Cookies["UserCookie"];
                
                httpClient.BaseAddress = new Uri(_configuration["urls:CustomerApi"]);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                
                var response = await httpClient.PostAsJsonAsync(_configuration["urls:CustomerApi"], newCustomer);

                if (response.StatusCode != System.Net.HttpStatusCode.Created)
                {
                    return NoContent();
                }
                return RedirectToAction(nameof(Index));
            }
        }

        return NoContent();
    }
    [HttpGet]
    public IActionResult CustomerView(Guid id)
    {
        var model = new CustomerModel();

        using (var httpClient = new HttpClient())
        {
            var accessToken = Request.Cookies["UserCookie"];
                
            httpClient.BaseAddress = new Uri($"{_configuration["urls:CustomerApi"]}/{id}");
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

            var response = httpClient.GetStringAsync($"{_configuration["urls:CustomerApi"]}/{id}").Result;

            model = JsonConvert.DeserializeObject<CustomerModel>(response);
        }
        
        return View(model);
    }
}