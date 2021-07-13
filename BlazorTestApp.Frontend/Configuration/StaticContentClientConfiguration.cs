using System;
using BlazorTestApp.Frontend.Clients;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorTestApp.Frontend.Configuration
{
    public static class StaticContentClientConfiguration
    {
        public static WebAssemblyHostBuilder AddStaticContentClient(this WebAssemblyHostBuilder builder)
        {
            builder.Services.AddHttpClient<IStaticContentClient, StaticContentClient>(client =>
            {
                client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
            });

            return builder;
        }
    }
}