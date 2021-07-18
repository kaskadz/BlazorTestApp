using Microsoft.AspNetCore.Authorization;

namespace BlazorTestApp.Shared.Authorization
{
    public static class AuthorizationOptionsExtensions
    {
        public static AuthorizationOptions AddNamedPolicy(this AuthorizationOptions options,
            NamedAuthorizationPolicy policy)
        {
            options.AddPolicy(policy.Name, policy.Policy);

            return options;
        }
    }
}