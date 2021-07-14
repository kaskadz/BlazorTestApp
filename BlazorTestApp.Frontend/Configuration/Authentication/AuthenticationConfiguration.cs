using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Authentication.WebAssembly.Msal.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorTestApp.Frontend.Configuration.Authentication
{
    public static class AuthenticationConfiguration
    {
        public const string RoleClaimType = "role";
        private const string AzureAdSection = "AzureAd";

        public static WebAssemblyHostBuilder AddAuthentication(this WebAssemblyHostBuilder builder)
        {
            builder.Services.AddMsalAuthentication<RemoteAuthenticationState, CustomUserAccount>(options =>
            {
                builder.Configuration.Bind(AzureAdSection, options.ProviderOptions);
                options.UserOptions.RoleClaim = RoleClaimType;
            }).AddAccountClaimsPrincipalFactory<RemoteAuthenticationState, CustomUserAccount, CustomAccountFactory>();

            return builder;
        }
    }
}