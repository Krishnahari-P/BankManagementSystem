using BankManagementSystem.Client.Dto;
using BankManagementSystem.Client.HttpClients;
using BankManagementSystem.Client.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System.Security.Claims;
public class AccountController : Controller
{
    private readonly IGenericHttpClient _client;
    private readonly IToastNotification _nToastNotify;

    public AccountController(IGenericHttpClient client,IToastNotification nToastNotify)
    {
        _client = client;
        _nToastNotify = nToastNotify;
    }

    [HttpGet]
    public IActionResult Login() => View();

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        try
        {
            var response = await _client.PostAsync<LoginResponse>(ApiConstant.Authenticate, model);

            if (response == null || string.IsNullOrEmpty(response.Token))
            {
                ModelState.AddModelError(string.Empty, "Invalid username or password");
                return View(model);
            }
            
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, response.UserName??String.Empty),
                new Claim("UserId", response.UserId??String.Empty),
                new Claim("JwtToken", response.Token)
            };

            if (!string.IsNullOrEmpty(response.CustomerId))
                claims.Add(new Claim("CustomerId", response.CustomerId));

            foreach (var role in response.Roles)
                claims.Add(new Claim(ClaimTypes.Role, role));

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.UtcNow.AddHours(1)
                });
            if (response.Roles.Contains("Admin") || response.Roles.Contains("Manager"))
                return RedirectToAction("Index", "Dashboard");

            else if (response.Roles.Contains("Customer"))
                return RedirectToAction("Index", "Dashboard");
            else
                return RedirectToAction("Index", "Home");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, $"Login failed: {ex.Message}");
            return View(model);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login");
    }

    [HttpGet]
    public IActionResult ChangePassword()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        try
        {
            await _client.PostAsync<object>(ApiConstant.ChangePassword, model);
            _nToastNotify.AddSuccessToastMessage("Password changed successfully!");
            return RedirectToAction("Index", "Dashboard");
        }
        catch (HttpRequestException)
        {
            _nToastNotify.AddErrorToastMessage("Error changing password. Please try again.");
            return View(model);
        }
        catch (Exception)
        {
            _nToastNotify.AddErrorToastMessage("Unexpected error occurred.");
            return View(model);
        }
    }

}
