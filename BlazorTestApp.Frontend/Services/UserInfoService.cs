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
            User user = await GetUserAsync();
            IEnumerable<string> groups = await GetGroupNamesAsync();
            IEnumerable<TokenDisplayData> tokens = await _tokenExtractionService.GetTokensAsync();
            AuthenticationState authenticationState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            ClaimsPrincipal principal = authenticationState.User;
            string login = principal.Identity?.Name;
            IEnumerable<string> roles = principal.Claims
                .Where(x => x.Type == "role")
                .Select(x => x.Value);
            string userIdClaim = principal.Claims
                .FirstOrDefault(x => x.Type == "userId")
                ?.Value;

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
    }
}