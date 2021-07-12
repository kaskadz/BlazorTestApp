using System;
using BlazorTestApp.Frontend.Clients;
using BlazorTestApp.Frontend.Options;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorTestApp.Frontend.Configuration
{
    public static class WebApiClientWebAssemblyHostBuilderExtensions
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