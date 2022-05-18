using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TimeReportApi.Data;
using TimeReportApi.DTO.CustomerDTOs;

namespace TimeReportApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomerController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CustomerController(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public IActionResult Index()
    {
        return Ok(_mapper.Map<List<CustomerDto>>(_context.Customers));
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public IActionResult Create(CreateCustomerDto customer)
    {
        var newCustomer = _mapper.Map<Customer>(customer);

        _context.Customers.Add(newCustomer);

        _context.SaveChanges();

        var customerDto = _mapper.Map<CustomerDto>(newCustomer);

        return CreatedAtAction(nameof(GetOne), new { id = customerDto.Id }, customerDto);
    }

    [HttpGet]
    [Route("{id}")]
    public IActionResult GetOne(string id)
    {
        var customer = _context.Customers.FirstOrDefault(cust => cust.Id.ToString() == id);
        if (customer == null) return NotFound("No customer found");

        var customerToReturn = _mapper.Map<CustomerDto>(customer);

        return Ok(customerToReturn);
    }

    [HttpPut]
    [Route("{id}")]
    [Authorize(Roles = "Admin")]
    public IActionResult EditCustomer(string id, EditCustomerDto editedCustomer)
    {
        var customer = _context.Customers.FirstOrDefault(cust => cust.Id.ToString() == id);
        if (customer == null) return NotFound("No customer found");

        _mapper.Map(editedCustomer, customer);

        _context.SaveChanges();

        var customerToReturn = _mapper.Map<CustomerDto>(customer);

        return Ok(customerToReturn);
    }

    [HttpDelete]
    [Route("{id}")]
    [Authorize(Roles = "Admin")]
    public IActionResult DeleteCustomer(string id)
    {
        var customer = _context.Customers.FirstOrDefault(cust => cust.Id.ToString() == id);

        if (customer == null) return NotFound("No customer found");

        _context.Customers.Remove(customer);

        _context.SaveChanges();

        return Ok($"Customer with the Id: {id} was deleted");
    }
}