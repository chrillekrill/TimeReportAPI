using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using TimeReportMvc.Models.CustomerModels;
using TimeReportMvc.Models.ProjectModels;

namespace TimeReportMvc.Controllers;

public class ProjectController : Controller
{
    private readonly IConfiguration _configuration;

    public ProjectController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    private CustomerViewModel getCustomerFromProjectId(Guid id)
    {
        using (var httpClient = new HttpClient())
        {
            var accessToken = Request.Cookies["UserCookie"];
            
            httpClient.BaseAddress = new Uri($"{_configuration["urls:CustomerApi"]}/{id}");
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            
            var response = httpClient.GetStringAsync($"{_configuration["urls:CustomerApi"]}/{id}").Result;

            return JsonConvert.DeserializeObject<CustomerViewModel>(response);
        }
    }
    // GET
    public IActionResult Index()
    {
        var model = new ProjectIndexModel();

        using (var httpClient = new HttpClient())
        {
            var accessToken = Request.Cookies["UserCookie"];
            
            httpClient.BaseAddress = new Uri(_configuration["urls:ProjectApi"]);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

            var data = httpClient.GetStringAsync(_configuration["urls:ProjectApi"]).Result;
            
            var newModel = JsonConvert.DeserializeObject<List<ProjectIndexModel.ProjectModel>>(data);
            
            model.Projects = newModel.Select(e => new ProjectIndexModel.ProjectModel
            {
                Id = e.Id,
                Name = e.Name
            }).ToList();
        }
        
        return View(model);
    }
    
    [HttpGet]
    public IActionResult ProjectView(Guid id)
    {
        var model = new ProjectViewModel();

        using (var httpClient = new HttpClient())
        {
            var accessToken = Request.Cookies["UserCookie"];
                
            httpClient.BaseAddress = new Uri($"{_configuration["urls:ProjectApi"]}/{id}");
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

            var response = httpClient.GetStringAsync($"{_configuration["urls:ProjectApi"]}/{id}").Result;

            model = JsonConvert.DeserializeObject<ProjectViewModel>(response);
            
            model.Customer = getCustomerFromProjectId(model.CustomerId);
        }
        
        return View(model);
    }
    [HttpGet]
    public IActionResult CreateProject()
    {
        var model = new ProjectNewModel();
        
        using (var httpClient = new HttpClient())
        {
            var accessToken = Request.Cookies["UserCookie"];
            
            httpClient.BaseAddress = new Uri(_configuration["urls:CustomerApi"]);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

            var data = httpClient.GetStringAsync(_configuration["urls:CustomerApi"]).Result;
            
            var newModel = JsonConvert.DeserializeObject<List<CustomerViewModel>>(data);

            model.AllCustomers = newModel.Select(cus => new SelectListItem
            {
                Text = cus.Name,
                Value = cus.Id.ToString()
            }).ToList();
            model.AllCustomers.Insert(0, new SelectListItem
            {
                Value = "",
                Text = "Please select a customer"
            });
        }

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProject(ProjectNewModel newProject)
    {
        if (ModelState.IsValid)
        {
            using (var httpClient = new HttpClient())
            {
                var accessToken = Request.Cookies["UserCookie"];
                
                httpClient.BaseAddress = new Uri(_configuration["urls:ProjectApi"]);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                
                var response = await httpClient.PostAsJsonAsync(_configuration["urls:ProjectApi"], newProject);

                if (response.StatusCode != System.Net.HttpStatusCode.Created)
                {
                    return NoContent();
                }
                return RedirectToAction(nameof(Index));
            }
        }

        return NoContent();
    }
}