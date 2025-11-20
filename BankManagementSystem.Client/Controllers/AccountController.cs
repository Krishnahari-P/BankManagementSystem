//using BankManagementSystem.Client.Dto;
//using BankManagementSystem.Client.HttpClients;
//using BankManagementSystem.Client.ViewModels;
//using Microsoft.AspNetCore.Authentication;
//using Microsoft.AspNetCore.Authentication.Cookies;
//using Microsoft.AspNetCore.Mvc;
//using System.Security.Claims;
//using System.Text;

//namespace BankManagementSystem.Client.Controllers
//{
//    public class AccountController : Controller
//    {
//        private readonly IGenericHttpClient _client;
//        private readonly ILogger<AccountController> _logger;

//        public AccountController(IGenericHttpClient client, ILogger<AccountController> logger)
//        {
//            this._client = client;
//            this._logger = logger;
//        }
//        public IActionResult Login()
//        {
//            return View();
//        }

//        //[HttpPost]
//        //public async Task<IActionResult> Login(LoginViewModel model)
//        //{
//        //    if (ModelState.IsValid)
//        //    {
//        //        var result = await _client.PostAsync<Result<UserResponse>>(ApiConstant.Authenticate, model);

//        //        if (result.IsError)
//        //        {
//        //            foreach (var err in result.Errors)
//        //            {
//        //                ModelState.AddModelError(string.Empty, err.ErrorMessage);
//        //            }
//        //        }
//        //        else
//        //        {
//        //            var encodedData = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{model.UserName}:{model.Password}"));
//        //            var claims = new List<Claim>
//        //             {
//        //                 new Claim(ClaimTypes.Name, model.UserName),
//        //                 new Claim("UserId", result.Response.Id),
//        //                 new Claim("basicauth", encodedData),
//        //             };

//        //            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

//        //            await HttpContext.SignInAsync(
//        //                CookieAuthenticationDefaults.AuthenticationScheme,
//        //                new ClaimsPrincipal(claimsIdentity),
//        //                new AuthenticationProperties
//        //                {
//        //                    IsPersistent = true
//        //                });

//        //            return RedirectToAction("Index", "Dashboard");
//        //        }


//        //    }
//        //    return View();
//        //}

//        [HttpPost]
//        public async Task<IActionResult> Login(LoginViewModel model)
//        {
//            if (!ModelState.IsValid)
//                return View(model);

//            try
//            {
//                // 1️⃣ Call API to get JWT token
//                var response = await _client.PostAsync<LoginResponse>(ApiConstant.Authenticate, model);

//                if (response == null || string.IsNullOrEmpty(response.Token))
//                {
//                    ModelState.AddModelError(string.Empty, "Invalid username or password");
//                    return View(model);
//                }

//                // 2️⃣ Create claims for local cookie authentication
//                var claims = new List<Claim>
//        {
//            new Claim(ClaimTypes.Name, response.UserName ?? ""),
//            new Claim("UserId", response.UserId ?? ""),
//            new Claim("JwtToken", response.Token ?? "")
//        };

//                if (response.Roles != null)
//                {
//                    foreach (var role in response.Roles)
//                        claims.Add(new Claim(ClaimTypes.Role, role));
//                }

//                // 3️⃣ Create identity and sign in user
//                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
//                var principal = new ClaimsPrincipal(identity);

//                await HttpContext.SignInAsync(
//                    CookieAuthenticationDefaults.AuthenticationScheme,
//                    principal,
//                    new AuthenticationProperties
//                    {
//                        IsPersistent = true,
//                        ExpiresUtc = DateTime.UtcNow.AddHours(1)
//                    });

//                // 4️⃣ Store JWT token for API calls
//                HttpContext.Session.SetString("JwtToken", response.Token);

//                // 5️⃣ Redirect based on role
//                if (response.Roles.Contains("Admin") || response.Roles.Contains("Manager"))
//                {
//                    return RedirectToAction("Index", "Dashboard");
//                }
//                else if (response.Roles.Contains("Customer"))
//                {
//                    return RedirectToAction("Index", "Dashboard");
//                }

//                return RedirectToAction("Index", "Home");
//            }
//            catch (Exception ex)
//            {
//                ModelState.AddModelError(string.Empty, $"Login failed: {ex.Message}");
//                return View(model);
//            }
//        }


//        public IActionResult Register()
//        {
//            return View();
//        }

//        [HttpPost]
//        public async Task<IActionResult> Register(RegisterViewModel model)
//        {
//            if (ModelState.IsValid)
//            {
//                var result = await _client.PostAsync<Result<UserResponse>>(ApiConstant.Register, new UserRequest
//                {
//                    UserName = model.UserName,
//                    Password = model.Password,
//                });

//                if (result.IsError)
//                {
//                    foreach (var error in result.Errors)
//                    {
//                        _logger.LogError(error.ErrorMessage);
//                    }
//                }
//                else
//                {
//                    return RedirectToAction(nameof(Login));
//                }
//            }
//            return View();
//        }
//    }
//}
using BankManagementSystem.Client.Dto;
using BankManagementSystem.Client.HttpClients;
using BankManagementSystem.Client.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System.Security.Claims;
using System.Text;

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
                new Claim(ClaimTypes.Name, response.UserName),
                new Claim("UserId", response.UserId),
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
        catch (HttpRequestException ex)
        {
            _nToastNotify.AddErrorToastMessage("Error changing password. Please try again.");
            return View(model);
        }
        catch (Exception ex)
        {
            _nToastNotify.AddErrorToastMessage("Unexpected error occurred.");
            return View(model);
        }
    }

}
