using Microsoft.AspNetCore.Identity;

namespace TimeReportApi.Models;

public class UserModel
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
}