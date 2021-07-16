using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication.Internal;

namespace BlazorTestApp.Frontend.Configuration.Authnz
{
    public class CustomAccountFactory : AccountClaimsPrincipalFactory<CustomUserAccount>
    {
        private readonly IGroupMappingProvider _groupMappingProvider;

        public CustomAccountFactory(
            IAccessTokenProviderAccessor accessor,
            IGroupMappingProvider groupMappingProvider) : base(accessor)
        {
            _groupMappingProvider = groupMappingProvider;
        }

        public override async ValueTask<ClaimsPrincipal> CreateUserAsync(
            CustomUserAccount account,
            RemoteAuthenticationUserOptions options)
        {
            ClaimsPrincipal initialUser = await base.CreateUserAsync(account, options);

            if (initialUser.Identity.IsAuthenticated)
            {
                var userIdentity = (ClaimsIdentity)initialUser.Identity;

                userIdentity.AddClaim(new Claim("userId", account.Oid));

                foreach (string groupId in account.Groups)
                {
                    if (_groupMappingProvider.IdToName.TryGetValue(groupId, out string groupName))
                    {
                        userIdentity.AddClaim(new Claim("role", groupName));
                    }
                }
            }

            return initialUser;
        }
    }
}