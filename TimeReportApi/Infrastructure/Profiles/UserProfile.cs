using AutoMapper;
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