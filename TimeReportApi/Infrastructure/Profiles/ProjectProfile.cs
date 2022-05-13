using AutoMapper;
using TimeReportApi.Data;
using TimeReportApi.DTO;
using TimeReportApi.DTO.ProjectDTOs;

namespace TimeReportApi.Infrastructure.Profiles
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<Project, ProjectDto>()
                .ReverseMap();
            CreateMap<Project, CreateProjectDto>()
                .ForMember(x => x.CustomerId, opt => opt.Ignore()).ReverseMap();
            CreateMap<Project, EditProjectDto>()
                .ReverseMap();
            CreateMap<Project, List<ProjectDto>>()
                .ReverseMap();
        }
    }
}
