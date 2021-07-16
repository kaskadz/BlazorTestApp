using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorTestApp.Frontend.Configuration.Authnz
{
    public static class AuthorizationConfiguration
    {
        public static WebAssemblyHostBuilder AddAuthorization(this WebAssemblyHostBuilder builder)
        {
            builder.Services.AddAuthorizationCore(options =>
            {
                options.AddPolicy(Policies.Weather, policy => policy.RequireRole("Weather"));
            });

            return builder;
        }
    }
}