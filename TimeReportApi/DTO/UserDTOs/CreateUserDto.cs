using System.ComponentModel.DataAnnotations;

namespace TimeReportApi.DTO.UserDTOs;

public class CreateUserDto
{
    [MaxLength(20)]
    [MinLength(4)]
    public string Username { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
}