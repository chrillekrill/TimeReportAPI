namespace TimeReportMvc.Models.CustomerModels;

public class CustomerIndexModel
{
    public List<CustomerModel> Customers { get; set; }

    public class CustomerModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}