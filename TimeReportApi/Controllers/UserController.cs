using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TimeReportApi.Data;
using TimeReportApi.DTO.UserDTOs;

namespace TimeReportApi.Controllers;

[Route("[controller]")]
[ApiController]
public class UserController : Controller
{
    private readonly IConfiguration _configuration;
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly UserManager<IdentityUser> _userManager;

    public UserController(IConfiguration configuration, UserManager<IdentityUser> userManager,
        ApplicationDbContext context, IMapper mapper)
    {
        _configuration = configuration;
        _userManager = userManager;
        _context = context;
        _mapper = mapper;
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Create(CreateUserDto newUser)
    {
        if (ModelState.IsValid)
        {
            var passWordCheck = newUser.Password.Any(x => char.IsLetter(x));
            if (_userManager.FindByEmailAsync(newUser.Username + "@gmail.com").Result != null)
                return BadRequest("User already exists");
            if (!Regex.IsMatch(newUser.Username, @"^[a-zA-Z]+$"))
                return BadRequest("Please enter a valid username");
            if (!passWordCheck)
                return BadRequest("Password must contain letters");




            var user = new IdentityUser
            {
                UserName = newUser.Username,
                Email = newUser.Username + "@gmail.com",
                EmailConfirmed = true
            };

            _userManager.CreateAsync(user, newUser.Password).Wait();
            _userManager.AddToRoleAsync(user, newUser.Role).Wait();

            _context.SaveChanges();

            return Ok("User created");
        }

        return BadRequest("Something went wrong");
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("login")]
    public IActionResult Login([FromBody] UserLoginDto userLogin)
    {
        var user = Authenticate(userLogin);

        if (user != null)
        {
            var token = Generate(user);

            var returnUser = _mapper.Map<UserDto>(userLogin);

            returnUser.Jwt = token;

            return Ok(returnUser);
        }

        return NotFound("Username or password is incorrect");
    }

    private string Generate(IdentityUser user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var userRole = _userManager.GetRolesAsync(user).Result;


        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.UserName),
            new Claim(ClaimTypes.Role, userRole.First())
        };

        var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
            _configuration["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddDays(30),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private IdentityUser Authenticate(UserLoginDto userLogin)
    {
        var currentUser = _userManager.FindByEmailAsync(userLogin.Username + "@gmail.com").Result;

        if (currentUser == null) return null;
        // var currentUser = Users.UserLogins.FirstOrDefault(u =>
        //     u.Username.ToLower() == userLogin.Username.ToLower() && u.Password == userLogin.Password);

        if (currentUser != null) return currentUser;

        return null;
    }
}