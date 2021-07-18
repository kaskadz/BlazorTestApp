using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BlazorTestApp.Frontend.Domain;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Graph;

namespace BlazorTestApp.Frontend.Services
{
    public class UserInfoService : IUserInfoService
    {
        private readonly IGraphServiceClient _graphServiceClient;
        private readonly ITokenExtractionService _tokenExtractionService;
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public UserInfoService(
            IGraphServiceClient graphServiceClient,
            ITokenExtractionService tokenExtractionService,
            AuthenticationStateProvider authenticationStateProvider)
        {
            _graphServiceClient = graphServiceClient;
            _tokenExtractionService = tokenExtractionService;
            _authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<UserInfoDisplayData> GetUserInfoDisplayData()
        {
            var getUserAsync = GetUserAsync();
            var getGroupNamesAsync = GetGroupNamesAsync();
            var getTokensAsync = _tokenExtractionService.GetTokensAsync();
            var getAuthenticationStateAsync = _authenticationStateProvider.GetAuthenticationStateAsync();

            await Task.WhenAll(getUserAsync, getGroupNamesAsync, getTokensAsync, getAuthenticationStateAsync);

            User user = getUserAsync.Result;
            IEnumerable<string> groups = getGroupNamesAsync.Result;
            IEnumerable<TokenDisplayData> tokens = getTokensAsync.Result;
            AuthenticationState authenticationState = getAuthenticationStateAsync.Result;

            ClaimsPrincipal principal = authenticationState.User;
            string login = GetLogin(principal);
            IEnumerable<string> roles = GetRoles(principal);
            string userIdClaim = GetUserIdClaim(principal);

            return new UserInfoDisplayData
            {
                Login = login,
                AzureAdObjectId = user.Id,
                UserIdClaim = userIdClaim,
                FirstName = user.GivenName,
                LastName = user.Surname,
                RoleClaims = roles,
                AzureAdGroups = groups,
                Tokens = tokens
            };
        }

        private async Task<IEnumerable<string>> GetGroupNamesAsync()
        {
            var groups = new List<string>();
            var pageRequest = _graphServiceClient.Me.MemberOf.Request()
                .Select("displayName");
            do
            {
                var page = await pageRequest.GetAsync();
                if (page != null)
                {
                    groups.AddRange(page.CurrentPage
                        .OfType<Group>()
                        .Select(x => x.DisplayName));
                }

                pageRequest = page?.NextPageRequest;
            } while (pageRequest != null);

            return groups;
        }

        private async Task<User> GetUserAsync()
        {
            return await _graphServiceClient.Me.Request()
                .Select(x => new
                    {
                        x.Id,
                        x.DisplayName,
                        x.GivenName,
                        x.Surname,
                    }
                )
                .GetAsync();
        }

        private static string GetLogin(ClaimsPrincipal principal)
        {
            return principal.Identity?.Name;
        }

        private static IEnumerable<string> GetRoles(ClaimsPrincipal principal)
        {
            return principal.Claims
                .Where(x => x.Type == "role")
                .Select(x => x.Value);
        }

        private static string GetUserIdClaim(ClaimsPrincipal principal)
        {
            return principal.Claims
                .FirstOrDefault(x => x.Type == "userId")
                ?.Value;
        }
    }
}