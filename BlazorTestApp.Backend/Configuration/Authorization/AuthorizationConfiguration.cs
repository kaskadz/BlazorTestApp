using BlazorTestApp.Shared.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorTestApp.Backend.Configuration.Authorization
{
    public static class AuthorizationConfiguration
    {
        public static IServiceCollection AddAadAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options
                    .AddNamedPolicy(Policies.Weather.NamedPolicy)
                    .AddNamedPolicy(Policies.Admin.NamedPolicy);
            });

            return services;
        }
    }
}