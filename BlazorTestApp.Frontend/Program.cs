using System.Threading.Tasks;
using BlazorTestApp.Frontend.Configuration;
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
                .AddWebApiClient();

            await builder.Build().RunAsync();
        }
    }
}