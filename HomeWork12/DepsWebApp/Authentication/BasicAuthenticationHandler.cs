using System;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using DepsWebApp.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;

namespace DepsWebApp.Authentication
{
    /// <summary>
    /// Basic Authentication Handler.
    /// </summary>
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IAuthService _authService;

        /// <summary>
        /// Constructor with DI.
        /// </summary>
        /// <param name="options"></param>
        /// <param name="logger"></param>
        /// <param name="encoder"></param>
        /// <param name="clock"></param>
        /// <param name="authService"></param>
        public BasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IAuthService authService) 
            : base(options, logger, encoder, clock)
        {
            _authService = authService;
        }
        /// <summary>
        /// Handle authentication.
        /// </summary>
        /// <returns></returns>
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var accountId = await GetAccountId(Request);
            if (accountId == null) return AuthenticateResult.NoResult();
            try
            {
                var identity = new ClaimsIdentity(
                    new[] {new Claim(ClaimTypes.NameIdentifier, accountId.ToString())},
                    Scheme.Name);
                return AuthenticateResult.Success(
                    new AuthenticationTicket(
                        new ClaimsPrincipal(identity),
                        Scheme.Name));
            }
            catch(Exception e)
            {
                Logger.LogError(e, "Auth error");
                return AuthenticateResult.Fail(e);
            }
        }

        private async Task<int?> GetAccountId(HttpRequest request)
        {
            
            if (request.Headers.ContainsKey(HeaderNames.Authorization))
            {
                var authHeader= request.Headers[HeaderNames.Authorization].FirstOrDefault();
                if (authHeader == null) return null;
                var userBytes = Convert.FromBase64String(authHeader);
                var userString = Encoding.UTF8.GetString(userBytes).Split(':');
                var login = userString[0];
                var password = userString[1];
                return await _authService.FindAsync(login, password);
            }

            return null;
        }
    }
}