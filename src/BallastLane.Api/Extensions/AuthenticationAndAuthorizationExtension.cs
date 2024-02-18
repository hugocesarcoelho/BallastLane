using BallastLane.Api.Handlers;

namespace BallastLane.Api.Extensions
{
    public static class AuthenticationAndAuthorizationExtension
    {

        public static void AddAuthenticationService(this IServiceCollection services)
        {
            services.AddAuthentication("BasicAuthentication").AddScheme<BasicAuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);
        }
    }
}
