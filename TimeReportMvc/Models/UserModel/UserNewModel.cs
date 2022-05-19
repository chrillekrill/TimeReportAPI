using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TimeReportMvc.Models.UserModel;

public class UserNewModel
{
    public string Username { get; set; }
    [DataType(DataType.Password)]
    public string Password { get; set; }
    public string Role { get; set; }
    
    public List<SelectListItem> AllRoles { get; set; } = new();

    public UserNewModel()
    {
        List<string> roles = new List<string>()
        {
            "Admin",
            "User"
        };

        AllRoles = roles.Select(role => new SelectListItem
        {
            Value = role,
            Text = role
        }).ToList();
        AllRoles.Insert(0, new SelectListItem
        {
            Value = "",
            Text = "Select a role"
        });
    }
}
