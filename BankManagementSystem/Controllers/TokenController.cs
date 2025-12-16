using BankManagementSystem.Entity.Dto;
using BankManagementSystem.Entity.Security;
using BankManagementSystem.Services.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[Route("api/[controller]")]
[ApiController]
public class TokenController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _configuration;
    private readonly ICustomerRepository _customerRepository;
    private readonly IUserRepository _userRepository;

    public TokenController(UserManager<ApplicationUser> userManager, IConfiguration configuration,ICustomerRepository customerRepository, IUserRepository userRepository)
    {
        _userManager = userManager;
        _configuration = configuration;
        _customerRepository = customerRepository;
        _userRepository = userRepository;
    }

    [HttpPost("GetToken")]
    public async Task<IActionResult> GetToken([FromBody] UserRequest request)
    {
        var authResult = await _userRepository.Authenticate(request);
        if (authResult.Response == null)
        {
            return Unauthorized("Invalid username or password.");
        }
        var user = await _userManager.FindByIdAsync(authResult.Response.Id);
        //var user = await _userManager.FindByNameAsync(request.UserName);
        var roles = await _userManager.GetRolesAsync(user);
        var customer = await _customerRepository.GetCustomerByUserIdAsync(user.Id);
        //if (customer == null)
        //{
        //    return NotFound("Account not found");
        //}
        if (user.IsActive==false)
        {
            return BadRequest("Access denied");
        }
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("UserId", user.Id),
            new Claim(ClaimTypes.Name, user.UserName),
        };
        if (customer != null)
        {
            claims.Add(new Claim("CustomerId", customer.CustomerId.ToString()));
        }
        foreach (var role in roles)
            claims.Add(new Claim(ClaimTypes.Role, role));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(Convert.ToDouble(_configuration["Jwt:ExpiryInHours"])),
            signingCredentials: creds
        );

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return Ok(new
        {
            Token = jwt,
            ExpiresIn = Convert.ToInt32(_configuration["Jwt:ExpiryInHours"]) * 3600,
            UserId = user.Id,
            UserName = user.UserName,
            Roles = roles,
            CustomerId = customer?.CustomerId
        });
    }
}
