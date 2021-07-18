using System;
using Microsoft.AspNetCore.Authorization;

namespace BlazorTestApp.Shared.Authorization
{
    public class NamedAuthorizationPolicy
    {
        public string Name { get; }
        public AuthorizationPolicy Policy { get; }

        private NamedAuthorizationPolicy(string name, AuthorizationPolicy policy)
        {
            Name = name;
            Policy = policy;
        }

        public static NamedAuthorizationPolicy Create(string name, Action<AuthorizationPolicyBuilder> policy)
        {
            var policyBuilder = new AuthorizationPolicyBuilder();
            policy(policyBuilder);
            return new NamedAuthorizationPolicy(name, policyBuilder.Build());
        }
    }
}