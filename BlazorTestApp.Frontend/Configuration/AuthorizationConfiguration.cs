using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorTestApp.Frontend.Configuration
{
    public static class AuthorizationConfiguration
    {
        public static WebAssemblyHostBuilder AddAuthorization(this WebAssemblyHostBuilder builder)
        {
            builder.Services.AddAuthorizationCore(options =>
            {
                options.AddPolicyForRole(PolicyNames.Admin, AadGroupIds.Administration);
                options.AddPolicyForRole(PolicyNames.Weather, AadGroupIds.Weather);
            });

            return builder;
        }

        private static void AddPolicyForRole(this AuthorizationOptions options,
            string policyName,
            string roleName)
        {
            options.AddPolicy(policyName, policy =>
                policy.RequireClaim(AuthenticationConfiguration.RoleClaimType, roleName));
        }

        public static class PolicyNames
        {
            public const string Admin = "Admin";
            public const string Weather = "Weather";
        }

        private static class AadGroupIds
        {
            public const string Administration = "7a188f9b-cbcc-4997-82cb-4923601282de";
            public const string Weather = "195d4a45-4de4-4600-89f3-72cae83e4d7b";
        }
    }
}