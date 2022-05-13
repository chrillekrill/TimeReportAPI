using TimeReportApi.DTO.CustomerDTOs;
using AutoMapper;
using TimeReportApi.Data;

namespace TimeReportApi.Infrastructure.Profiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerDto>()
                .ReverseMap();
            CreateMap<Customer, CreateCustomerDto>()
                .ReverseMap();
            CreateMap<Customer, EditCustomerDto>()
                .ReverseMap();
            CreateMap<Customer, List<CustomerDto>>()
                .ReverseMap();
        }
    }
}
