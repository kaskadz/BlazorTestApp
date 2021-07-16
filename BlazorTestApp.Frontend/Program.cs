using System.Threading.Tasks;
using Blazored.SessionStorage;
using BlazorTestApp.Frontend.Configuration.Authnz;
using BlazorTestApp.Frontend.Configuration.Clients;
using BlazorTestApp.Frontend.Configuration.GraphClient;
using BlazorTestApp.Frontend.Configuration.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace BlazorTestApp.Frontend
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder
                .AddAuthentication()
                .AddAuthorization()
                .AddStaticContentClient()
                .AddWebApiClient()
                .AddGraphClient()
                .AddServices();

            builder.Services.AddBlazoredSessionStorage();

            await builder.Build().RunAsync();
        }
    }
}