using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Authentication.WebAssembly.Msal.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Graph;

namespace BlazorTestApp.Frontend.Configuration.GraphClient
{
    public static class GraphClientConfiguration
    {
        private const string BearerTokenScheme = "Bearer";

        public static WebAssemblyHostBuilder AddGraphClient(this WebAssemblyHostBuilder builder)
        {
            builder.Services.AddOptions<GraphClientOptions>().BindConfiguration(GraphClientOptions.Section);

            builder.Services.AddScoped<IHttpProvider, HttpClientHttpProvider>(sp =>
                new HttpClientHttpProvider(new HttpClient()));
            builder.Services.AddScoped<IGraphServiceClient>(sp => new GraphServiceClient(
                new DelegateAuthenticationProvider(async request =>
                {
                    var accessTokenProvider = sp.GetRequiredService<IAccessTokenProvider>();
                    var graphClientOptions = sp.GetRequiredService<IOptions<GraphClientOptions>>();
                    GraphClientOptions graphClientOptionsValue = graphClientOptions.Value;
                    AccessTokenResult result = await accessTokenProvider.RequestAccessToken(
                        new AccessTokenRequestOptions
                        {
                            Scopes = graphClientOptionsValue.Scopes
                        });

                    if (result.TryGetToken(out var token))
                    {
                        request.Headers.Authorization = new AuthenticationHeaderValue(
                            BearerTokenScheme, token.Value);
                    }
                }), sp.GetRequiredService<IHttpProvider>()));

            return builder;
        }
    }
}