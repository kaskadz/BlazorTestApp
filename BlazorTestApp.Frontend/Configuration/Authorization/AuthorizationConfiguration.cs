using BlazorTestApp.Frontend.Configuration.Authentication;
using BlazorTestApp.Shared.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorTestApp.Frontend.Configuration.Authorization
{
    public static class AuthorizationConfiguration
    {
        public static WebAssemblyHostBuilder AddAuthorization(this WebAssemblyHostBuilder builder)
        {
            builder.Services.AddAuthorizationCore(options =>
            {
                options
                    .AddNamedPolicy(Policies.Weather.NamedPolicy)
                    .AddNamedPolicy(Policies.Admin.NamedPolicy)
                    .AddNamedPolicy(Policies.Player.NamedPolicy)
                    .AddNamedPolicy(Policies.Host.NamedPolicy)
                    .AddNamedPolicy(Policies.Editor.NamedPolicy);
            });

            return builder;
        }
    }
}