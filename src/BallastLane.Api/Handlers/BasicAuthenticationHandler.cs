using BallastLane.ApplicationService.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Data;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace BallastLane.Api.Handlers
{
    public class BasicAuthenticationHandler : AuthenticationHandler<BasicAuthenticationSchemeOptions>
    {
        private readonly IUserAppService _userAppService;

        public BasicAuthenticationHandler(
            IOptionsMonitor<BasicAuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IUserAppService userApplicationService) : base(options, logger, encoder, clock)
        {
            _userAppService = userApplicationService;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var authorizationHeader = Request.Headers["Authorization"].ToString();
            if (authorizationHeader != null && authorizationHeader.StartsWith("basic", StringComparison.OrdinalIgnoreCase))
            {
                var token = authorizationHeader.Substring("Basic ".Length).Trim();
                var credentialsAsEncodedString = Encoding.UTF8.GetString(Convert.FromBase64String(token));
                var credentials = credentialsAsEncodedString.Split(':');

                var isValidCredentials = await _userAppService.ValidateAsync(credentials[0], credentials[1]);

                if (isValidCredentials)
                {
                    var claims = new List<Claim>();

                    var user = await _userAppService.GetByUsernameAsync(credentials[0]);

                    if (user.IsAdmin)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, "admin"));
                    }

                    var identity = new ClaimsIdentity(claims, "Basic");

                    var claimsPrincipal = new ClaimsPrincipal(identity);

                    return await Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, Scheme.Name)));
                }
            }

            Response.StatusCode = 401;
            return await Task.FromResult(AuthenticateResult.Fail("Invalid Authorization Header"));
        }
    }
}
