using BlazorTestApp.Frontend.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorTestApp.Frontend.Configuration.Services
{
    public static class ServicesConfiguration
    {
        public static WebAssemblyHostBuilder AddServices(this WebAssemblyHostBuilder builder)
        {
            builder.Services
                .AddTransient<IUserInfoService, UserInfoService>()
                .AddTransient<ITokenExtractionService, TokenExtractionService>();

            return builder;
        }
    }
}