using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NToastNotify;
using TimeReportMvc.Models.CustomerModels;
using TimeReportMvc.Models.ProjectModels;

namespace TimeReportMvc.Controllers;

[Authorize(Roles = "Admin")]
public class CustomerController : Controller
{
    private readonly IConfiguration _configuration;
    private readonly IToastNotification _toastNotification;

    public CustomerController(IConfiguration configuration, IToastNotification toastNotification)
    {
        _configuration = configuration;
        _toastNotification = toastNotification;
    }

    private List<ProjectIndexModel.ProjectModel> GetProjectsFromCustomerId(Guid id)
    {
        using (var httpClient = new HttpClient())
        {
            var accessToken = Request.Cookies["UserCookie"];

            httpClient.BaseAddress = new Uri($"{_configuration["urls:ProjectApi"]}/customer/{id}");
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

            var response = httpClient.GetStringAsync($"{_configuration["urls:ProjectApi"]}/customer/{id}").Result;


            var projects = JsonConvert.DeserializeObject<List<ProjectIndexModel.ProjectModel>>(response);

            return projects;
        }
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

            var newModel = JsonConvert.DeserializeObject<List<CustomerIndexModel.CustomerModel>>(data);

            model.Customers = newModel.Select(e => new CustomerIndexModel.CustomerModel
            {
                Name = e.Name,
                Id = e.Id
            }).ToList();
        }

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> CustomerDelete(Guid id)
    {
        using (var httpClient = new HttpClient())
        {
            var accessToken = Request.Cookies["UserCookie"];
            
            httpClient.BaseAddress = new Uri($"{_configuration["urls:CustomerApi"]}/{id}");
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

            await httpClient.DeleteAsync($"{_configuration["urls:CustomerApi"]}/{id}");

            _toastNotification.AddSuccessToastMessage("Customer deleted");
            return RedirectToAction(nameof(Index));
        }
    }

    [HttpGet]
    public IActionResult CustomerCreate() //OnGet
    {
        var model = new CustomerNewModel();
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> CustomerCreate(CustomerNewModel newCustomer)
    {
        if (ModelState.IsValid)
            using (var httpClient = new HttpClient())
            {
                var accessToken = Request.Cookies["UserCookie"];

                httpClient.BaseAddress = new Uri(_configuration["urls:CustomerApi"]);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

                var response = await httpClient.PostAsJsonAsync(_configuration["urls:CustomerApi"], newCustomer);

                if (response.StatusCode != HttpStatusCode.Created) return NoContent();

                _toastNotification.AddSuccessToastMessage("Customer created");
                return RedirectToAction(nameof(Index));
            }

        if (newCustomer.Name == null)
        {
            _toastNotification.AddErrorToastMessage("Please write a name for the customer");
            return View();
        }
        return NoContent();
    }

    [HttpGet]
    public IActionResult CustomerView(Guid id)
    {
        var model = new CustomerViewModel();

        using (var httpClient = new HttpClient())
        {
            var accessToken = Request.Cookies["UserCookie"];

            httpClient.BaseAddress = new Uri($"{_configuration["urls:CustomerApi"]}/{id}");
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

            var response = httpClient.GetStringAsync($"{_configuration["urls:CustomerApi"]}/{id}").Result;

            model = JsonConvert.DeserializeObject<CustomerViewModel>(response);
            var projects = GetProjectsFromCustomerId(model.Id);

            model.Projects = projects;
        }

        return View(model);
    }

    [HttpGet]
    public IActionResult CustomerEdit(Guid id)
    {
        var model = new CustomerEditModel();
//        model.CustomerId = id;
        using (var httpClient = new HttpClient())
        {
            var accessToken = Request.Cookies["UserCookie"];

            httpClient.BaseAddress = new Uri($"{_configuration["urls:CustomerApi"]}/{id}");
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

            var response = httpClient.GetStringAsync($"{_configuration["urls:CustomerApi"]}/{id}").Result;

            model = JsonConvert.DeserializeObject<CustomerEditModel>(response);
        }

        model.CustomerId = id;

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> CustomerEdit(CustomerEditModel editCustomer)
    {
        if (ModelState.IsValid)
            using (var httpClient = new HttpClient())
            {
                var accessToken = Request.Cookies["UserCookie"];

                httpClient.BaseAddress = new Uri($"{_configuration["urls:CustomerApi"]}/{editCustomer.CustomerId}");
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

                //var json = JsonConvert.SerializeObject(editCustomer);

                await httpClient.PutAsJsonAsync($"{_configuration["urls:CustomerApi"]}/{editCustomer.CustomerId}", new
                {
                    editCustomer.Name
                });

                _toastNotification.AddSuccessToastMessage("Customer edited. New name is " + editCustomer.Name);
                return RedirectToAction(nameof(CustomerView), new { id = editCustomer.CustomerId});
            }
        _toastNotification.AddErrorToastMessage("Please write a valid name");
        return View(editCustomer);
    }
}