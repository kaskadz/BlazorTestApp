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
            IEnumerable<string> groups = await GetGroupsAsync();
            IEnumerable<TokenDisplayData> tokens = await _tokenExtractionService.GetTokensAsync();
            AuthenticationState authenticationState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            ClaimsPrincipal principal = authenticationState.User;
            string login = principal.Identity?.Name;
            IEnumerable<string> roles = principal.Claims
                .Where(x => x.Type == "role")
                .Select(x => x.Value);

            return new UserInfoDisplayData
            {
                Login = login,
                Identifier = user.Id,
                FirstName = user.GivenName,
                LastName = user.Surname,
                Roles = roles,
                Groups = groups,
                Tokens = tokens
            };
        }

        private async Task<IEnumerable<string>> GetGroupsAsync()
        {
            // _graphServiceClient.Me.MemberOf.Request()
            //     .Select(x => new
            //         {
            //             (x as Group).DisplayName
            //         }
            //     ).GetAsync();
            var groups = new List<string>();
            var pageRequest = _graphServiceClient.Me
                .GetMemberGroups(securityEnabledOnly: true).Request();
            do
            {
                var page = await pageRequest.PostAsync();
                if (page != null)
                {
                    groups.AddRange(page.CurrentPage);
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