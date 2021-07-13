using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorTestApp.Frontend.Configuration.Authentication
{
    public static class AuthenticationConfiguration
    {
        public const string RoleClaimType = "role";

        public static WebAssemblyHostBuilder AddAuthentication(this WebAssemblyHostBuilder builder)
        {
            builder.Services.AddMsalAuthentication<RemoteAuthenticationState, CustomUserAccount>(options =>
                        {
                            builder.Configuration.Bind("AzureAd", options.ProviderOptions);
                            options.UserOptions.RoleClaim = RoleClaimType;
                        }).AddAccountClaimsPrincipalFactory<RemoteAuthenticationState, CustomUserAccount, CustomAccountFactory>();
            
            return builder;
        }
    }
}