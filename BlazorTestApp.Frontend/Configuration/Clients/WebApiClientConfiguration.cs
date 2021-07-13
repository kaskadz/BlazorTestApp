using BlazorTestApp.Frontend.Clients;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorTestApp.Frontend.Configuration.Clients
{
    public static class WebApiClientConfiguration
    {
        public static WebAssemblyHostBuilder AddWebApiClient(this WebAssemblyHostBuilder builder)
        {
            var webApiOptions = builder.Configuration
                .GetSection(WebApiOptions.Section)
                .Get<WebApiOptions>();

            builder.Services.AddSingleton(webApiOptions);
            builder.Services.AddTransient<WebApiAuthorizationMessageHandler>();

            builder.Services.AddHttpClient<IWebApiClient, WebApiClient>(client =>
            {
                client.BaseAddress = webApiOptions.Url;
            }).AddHttpMessageHandler<WebApiAuthorizationMessageHandler>();

            return builder;
        }
    }
}