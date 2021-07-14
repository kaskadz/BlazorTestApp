using System.Threading.Tasks;
using BlazorTestApp.Frontend.Configuration;
using BlazorTestApp.Frontend.Configuration.Authentication;
using BlazorTestApp.Frontend.Configuration.Authorization;
using BlazorTestApp.Frontend.Configuration.Clients;
using BlazorTestApp.Frontend.Configuration.GraphClient;
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
                .AddGraphClient();

            await builder.Build().RunAsync();
        }
    }
}