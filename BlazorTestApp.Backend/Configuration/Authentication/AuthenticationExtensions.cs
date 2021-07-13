using BlazorTestApp.Backend.Configuration.GraphClient;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Web;

namespace BlazorTestApp.Backend.Configuration.Authentication
{
    public static class AuthenticationExtensions
    {
        public static IServiceCollection AddAadAuthentication(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<JwtBearerOptions>(
                JwtBearerDefaults.AuthenticationScheme,
                options => { options.TokenValidationParameters.NameClaimType = "name"; });

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApi(configuration, AzureAdOptions.Section);

            return services;
        }
    }
}