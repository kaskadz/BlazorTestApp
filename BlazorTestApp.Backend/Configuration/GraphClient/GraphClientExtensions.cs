using System.Net.Http.Headers;
using System.Threading;
using Azure.Core;
using Azure.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Graph;

namespace BlazorTestApp.Backend.Configuration.GraphClient
{
    internal static class GraphClientExtensions
    {
        private const string GraphApiDefaultScope = "https://graph.microsoft.com/.default";

        public static IServiceCollection AddAadGraphClient(this IServiceCollection services)
        {
            services.AddOptions<AzureAdOptions>().BindConfiguration(AzureAdOptions.Section);
            services.AddScoped(sp =>
            {
                var options = sp.GetRequiredService<IOptions<AzureAdOptions>>();
                AzureAdOptions azureAdOptions = options.Value;

                TokenCredential credential = new ClientSecretCredential(
                    azureAdOptions.TenantId, azureAdOptions.ClientId, azureAdOptions.Secret);
                
                return new GraphServiceClient(
                    new DelegateAuthenticationProvider(async requestMessage =>
                    {
                        AccessToken token = await credential.GetTokenAsync(
                            new TokenRequestContext(new[] { GraphApiDefaultScope }),
                            CancellationToken.None);

                        requestMessage.Headers.Authorization =
                            new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, token.Token);
                    }));
            });

            return services;
        }
    }
}