using BankManagementSystem.Services.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace BankManagementSystem.Handlers
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IUserRepository _userRepository;

        public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock,IUserRepository userRepository) 
            : base(options, logger, encoder, clock)
        {
            _userRepository = userRepository;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            try
            {
                var authInfo = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                var decodedCredebtial = Encoding.UTF8.GetString(Convert.FromBase64String(authInfo.Parameter));
                String[] usernamePassword = decodedCredebtial.Split(':');
                if (await _userRepository.IsAValidUser(usernamePassword.FirstOrDefault(), usernamePassword.LastOrDefault()))
                {
                    return AuthenticateResult.Fail("Invalid User");
                }
                Claim[] claims = { new Claim(ClaimTypes.Name, usernamePassword.FirstOrDefault()) };
                var identity = new ClaimsIdentity(claims, Scheme.Name);
                var principle = new ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(principle, Scheme.Name);
                return AuthenticateResult.Success(ticket);
            }
            catch (Exception)
            {
                return AuthenticateResult.Fail("Invalid User");
            }
        }


    }
}
