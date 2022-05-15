using AutoMapper;
using Microsoft.AspNetCore.Identity;
using TimeReportApi.DTO.TimeReportDTOs;
using TimeReportApi.DTO.UserDTOs;

namespace TimeReportApi.Infrastructure.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserDto, UserLoginDto>()
            .ReverseMap();
    }
}