using BlazorTestApp.Frontend.Clients;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace BlazorTestApp.Frontend.Configuration.Clients
{
    public static class WebApiClientConfiguration
    {
        public static WebAssemblyHostBuilder AddWebApiClient(this WebAssemblyHostBuilder builder)
        {
            builder.Services.AddOptions<WebApiOptions>().BindConfiguration(WebApiOptions.Section);

            builder.Services.AddTransient<WebApiAuthorizationMessageHandler>();

            builder.Services.AddHttpClient<IWebApiClient, WebApiClient>((sp, client) =>
            {
                var webApiOptions = sp.GetRequiredService<IOptions<WebApiOptions>>();
                WebApiOptions webApiOptionsValue = webApiOptions.Value;
                client.BaseAddress = webApiOptionsValue.Url;
            }).AddHttpMessageHandler<WebApiAuthorizationMessageHandler>();

            return builder;
        }
    }
}