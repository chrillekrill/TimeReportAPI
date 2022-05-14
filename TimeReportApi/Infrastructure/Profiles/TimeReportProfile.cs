using AutoMapper;
using TimeReportApi.Data;
using TimeReportApi.DTO.TimeReportDTOs;

namespace TimeReportApi.Infrastructure.Profiles;

public class TimeReportProfile : Profile
{
    public TimeReportProfile()
    {
        CreateMap<TimeReport, TimeReportDto>()
            .ReverseMap();
        CreateMap<TimeReport, CreateTimeReportDto>()
            .ForMember(x => x.CustomerId, opt => opt.Ignore()).ForMember(x => x.ProjectId, opt => opt.Ignore()).ReverseMap();
        CreateMap<TimeReport, List<TimeReportDto>>()
            .ReverseMap();
        CreateMap<TimeReport, EditTimeReportDto>()
            .ReverseMap();
    }
}