namespace TimeReportApi.Models;

public class Users
{
    //public static UserModel User = new UserModel() { Username = "Stefan", Password = "Hejsan123#", Role="Admin" };

    public static List<UserModel> UserLogins = new List<UserModel>()
    {
        new UserModel() { Username = "Stefan", Password = "Hejsan123#", Role = "Admin" }
    };
}