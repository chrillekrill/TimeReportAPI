using AutoMapper;
using TimeReportApi.Data;
using TimeReportApi.DTO.CustomerDTOs;

namespace TimeReportApi.Infrastructure.Profiles;

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