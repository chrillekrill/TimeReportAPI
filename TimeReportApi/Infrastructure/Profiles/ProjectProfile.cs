using AutoMapper;
using TimeReportApi.Data;
using TimeReportApi.DTO.ProjectDTOs;

namespace TimeReportApi.Infrastructure.Profiles;

public class ProjectProfile : Profile
{
    public ProjectProfile()
    {
        CreateMap<Project, ProjectDto>()
            .ForMember(x => x.CustomerId, opt => opt.MapFrom(src => src.customer.Id)).ReverseMap();
        CreateMap<Project, CreateProjectDto>()
            .ForMember(x => x.CustomerId, opt => opt.MapFrom(src => src.customer.Id)).ReverseMap();
        CreateMap<Project, EditProjectDto>()
            .ReverseMap();
        CreateMap<Project, List<ProjectDto>>()
            .ReverseMap();
    }
}