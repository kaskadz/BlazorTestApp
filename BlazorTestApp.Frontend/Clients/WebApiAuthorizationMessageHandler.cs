using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using BlazorTestApp.Frontend.Configuration.Clients;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Extensions.Options;

namespace BlazorTestApp.Frontend.Clients
{
    public class WebApiAuthorizationMessageHandler : AuthorizationMessageHandler
    {
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public WebApiAuthorizationMessageHandler(
            IAccessTokenProvider provider,
            NavigationManager navigation,
            IOptions<WebApiOptions> webApiOptions,
            AuthenticationStateProvider authenticationStateProvider) : base(provider, navigation)
        {
            _authenticationStateProvider = authenticationStateProvider;
            WebApiOptions webApiOptionsValue = webApiOptions.Value;
            ConfigureHandler(
                authorizedUrls: new[] { webApiOptionsValue.Url.ToString() }
            );
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            AuthenticationState authenticationState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            if (authenticationState.User.Identity is { IsAuthenticated: false })
            {
                throw new UserNotAuthenticatedWebApiClientException();
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}