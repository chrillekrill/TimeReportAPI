using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using NToastNotify;
using TimeReportMvc.Models.CustomerModels;
using TimeReportMvc.Models.ProjectModels;
using TimeReportMvc.Models.TimeReportModels;

namespace TimeReportMvc.Controllers;
[Authorize(Roles = "Admin")]
public class ProjectController : Controller
{
    private readonly IConfiguration _configuration;
    private readonly IToastNotification _toastNotification;

    public ProjectController(IConfiguration configuration, IToastNotification toastNotification)
    {
        _configuration = configuration;
        _toastNotification = toastNotification;
    }

    private CustomerViewModel GetCustomerFromProjectId(Guid id)
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
            model.TimeReports = getTimeReportsFromId(id);

            model.Customer = GetCustomerFromProjectId(model.CustomerId);
        }

        return View(model);
    }

    private List<TimeReportIndexModel.TimeReportModel> getTimeReportsFromId(Guid id)
    {
        using (var httpClient = new HttpClient())
        {
            var accessToken = Request.Cookies["UserCookie"];

            httpClient.BaseAddress = new Uri($"{_configuration["urls:TimeReportApi"]}/project/{id}");
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

            var response = httpClient.GetStringAsync($"{_configuration["urls:TimeReportApi"]}/project/{id}").Result;
            
            var timeReports = JsonConvert.DeserializeObject<List<TimeReportIndexModel.TimeReportModel>>(response);

            return timeReports;
        }
    }

    private ProjectNewModel setProjectInfo()
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

        return model;
    }
    [HttpGet]
    public IActionResult ProjectCreate()
    {
        return View(setProjectInfo());
    }

    [HttpPost]
    public async Task<IActionResult> ProjectCreate(ProjectNewModel newProject)
    {
        if (ModelState.IsValid)
            using (var httpClient = new HttpClient())
            {
                var accessToken = Request.Cookies["UserCookie"];

                httpClient.BaseAddress = new Uri(_configuration["urls:ProjectApi"]);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

                var response = await httpClient.PostAsJsonAsync(_configuration["urls:ProjectApi"], newProject);

                if (response.StatusCode != HttpStatusCode.Created) return NoContent();

                _toastNotification.AddSuccessToastMessage("Project created");
                return RedirectToAction(nameof(Index));
            }

        if (newProject.CustomerId == Guid.Empty)
        {
            _toastNotification.AddErrorToastMessage("Please select a customer");
            return View(setProjectInfo());
        }

        return View(setProjectInfo());
    }
    [HttpPost]
    public async Task<IActionResult> ProjectDelete(Guid id)
    {
        using (var httpClient = new HttpClient())
        {
            var accessToken = Request.Cookies["UserCookie"];
            
            httpClient.BaseAddress = new Uri($"{_configuration["urls:ProjectApi"]}/{id}");
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

            await httpClient.DeleteAsync($"{_configuration["urls:ProjectApi"]}/{id}");
            _toastNotification.AddSuccessToastMessage("Project deleted");
            return RedirectToAction(nameof(Index));
        }
    }

    [HttpGet]
    public IActionResult ProjectEdit(Guid id)
    {
        var model = new ProjectEditModel();
//        model.CustomerId = id;
        using (var httpClient = new HttpClient())
        {
            var accessToken = Request.Cookies["UserCookie"];

            httpClient.BaseAddress = new Uri($"{_configuration["urls:ProjectApi"]}/{id}");
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

            var response = httpClient.GetStringAsync($"{_configuration["urls:ProjectApi"]}/{id}").Result;

            model = JsonConvert.DeserializeObject<ProjectEditModel>(response);
        }

        model.ProjectId = id;

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> ProjectEdit(ProjectEditModel editProject)
    {
        if (ModelState.IsValid)
            using (var httpClient = new HttpClient())
            {
                var accessToken = Request.Cookies["UserCookie"];

                httpClient.BaseAddress = new Uri($"{_configuration["urls:ProjectApi"]}/{editProject.ProjectId}");
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

                //var json = JsonConvert.SerializeObject(editCustomer);

                await httpClient.PutAsJsonAsync($"{_configuration["urls:ProjectApi"]}/{editProject.ProjectId}", new
                {
                    editProject.Name
                });
                _toastNotification.AddSuccessToastMessage("Project edited. New name is " + editProject.Name);
                return RedirectToAction(nameof(ProjectView), new { id = editProject.ProjectId});
            }
        _toastNotification.AddErrorToastMessage("Please write a valid name");
        return View();
    }
}