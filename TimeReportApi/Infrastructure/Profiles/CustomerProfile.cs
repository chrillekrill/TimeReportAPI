using TimeReportApi.DTO;
using AutoMapper;
using TimeReportApi.Data;

namespace TimeReportApi.Infrastructure.Profiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerDTO>()
                .ReverseMap();
            CreateMap<Customer, CreateCustomerDTO>()
                .ReverseMap();
            CreateMap<Customer, EditCustomerDTO>()
                .ReverseMap();
            CreateMap<Customer, List<CustomerDTO>>()
                .ReverseMap();
        }
    }
}
