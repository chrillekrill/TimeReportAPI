using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TimeReportApi.Data;
using TimeReportApi.DTO;

namespace TimeReportApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public CustomerController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return Ok(mapper.Map<List<CustomerDTO>>(context.Customers));
        }
        [HttpPost]
        [Route("create")]
        public IActionResult Create(CreateCustomerDTO customer)
        {
            var newCustomer = mapper.Map<Customer>(customer);

            newCustomer.Id = new Guid();

            context.Customers.Add(newCustomer);

            context.SaveChanges();

            var customerDTO = mapper.Map<CustomerDTO>(newCustomer);

            return CreatedAtAction(nameof(GetOne), new { id = newCustomer.Id }, customerDTO);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetOne(string id)
        {
            Guid guidId;
            try
            {
                guidId = Guid.Parse(id);
            }
            catch
            {
                return NotFound("No customer found");
            }

            var customer = context.Customers.FirstOrDefault(cust => cust.Id == guidId);
            if(customer == null) return NotFound("No customer found");

            var customerToReturn = mapper.Map<CustomerDTO>(customer);

            return Ok(customerToReturn);
        }
        [HttpPut]
        [Route("edit/{id}")]
        public IActionResult EditCustomer(string id, EditCustomerDTO editedCustomer)
        {

            Guid guidId;
            try
            {
               guidId = Guid.Parse(id);
            }
            catch
            {
               return NotFound("No customer found");
            }

            var customer = context.Customers.FirstOrDefault(cust => cust.Id == guidId);
            if (customer == null) return NotFound("No customer found");

            mapper.Map(editedCustomer, customer);

            context.SaveChanges();

            var customerToReturn = mapper.Map<CustomerDTO>(customer);

            return Ok(customerToReturn);
        }
        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult DeleteCustomer(string id)
        {

            Guid guidId;
            try
            {
                guidId = Guid.Parse(id);
            } 
            catch
            {
                return NotFound("No customer found");
            }

            var customer = context.Customers.FirstOrDefault(cust => cust.Id == guidId);

            if(customer == null) return NotFound("No customer found");

            context.Customers.Remove(customer);

            context.SaveChanges();

            return Ok($"Customer with the Id: {guidId} was deleted");
        }
    }
}
