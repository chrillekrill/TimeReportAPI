using AutoMapper;
using TimeReportApi.Data;
using TimeReportApi.DTO.TimeReportDTOs;

namespace TimeReportApi.Infrastructure.Profiles;

public class TimeReportProfile : Profile
{
    public TimeReportProfile()
    {
        CreateMap<TimeReport, TimeReportDto>()
            .ForMember(x => x.ProjectId, opt => opt.MapFrom(src => src.Project.Id)).ReverseMap();
        CreateMap<TimeReport, CreateTimeReportDto>()
            .ForMember(x => x.ProjectId, opt => opt.MapFrom(src => src.Project.Id)).ReverseMap();
        CreateMap<TimeReport, List<TimeReportDto>>()
            .ReverseMap();
        CreateMap<TimeReport, EditTimeReportDto>()
            .ReverseMap();
    }
}