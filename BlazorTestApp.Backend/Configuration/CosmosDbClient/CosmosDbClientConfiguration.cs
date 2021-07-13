using Microsoft.Azure.Cosmos.Fluent;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace BlazorTestApp.Backend.Configuration.CosmosDbClient
{
    public static class CosmosDbClientConfiguration
    {
        public static IServiceCollection AddCosmosDbClient(this IServiceCollection services)
        {
            services.AddOptions<CosmosDbOptions>().BindConfiguration(CosmosDbOptions.Section);
            services.AddSingleton(sp =>
            {
                var options = sp.GetRequiredService<IOptions<CosmosDbOptions>>();
                CosmosDbOptions cosmosDbOptions = options.Value;
                return new CosmosClientBuilder(cosmosDbOptions.ConnectionString).Build();
            });

            return services;
        }
    }
}