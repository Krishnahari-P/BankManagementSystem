using BankManagementSystem.Client.Dto;
using BankManagementSystem.Client.HttpClients;
using BankManagementSystem.Client.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;

namespace BankManagementSystem.Client.Controllers
{
    public class AccountController : Controller
    {
        private readonly IGenericHttpClient _client;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IGenericHttpClient client, ILogger<AccountController> logger)
        {
            this._client = client;
            this._logger = logger;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _client.PostAsync<Result<UserResponse>>(ApiConstant.Authenticate, model);

                if (result.IsError)
                {
                    foreach (var err in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, err.ErrorMessage);
                    }
                }
                else
                {
                    var encodedData = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{model.UserName}:{model.Password}"));
                    var claims = new List<Claim>
                     {
                         new Claim(ClaimTypes.Name, model.UserName),
                         new Claim("UserId", result.Response.Id),
                         new Claim("basicauth", encodedData),
                     };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        new AuthenticationProperties
                        {
                            IsPersistent = true
                        });

                    return RedirectToAction("Index", "Dashboard");
                }


            }
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _client.PostAsync<Result<UserResponse>>(ApiConstant.Register, new UserRequest
                {
                    UserName = model.UserName,
                    Password = model.Password,
                });

                if (result.IsError)
                {
                    foreach (var error in result.Errors)
                    {
                        _logger.LogError(error.ErrorMessage);
                    }
                }
                else
                {
                    return RedirectToAction(nameof(Login));
                }
            }
            return View();
        }
    }
}
