namespace BlazorTestApp.Backend.Configuration.CosmosDbClient
{
    public class CosmosDbOptions
    {
        public const string Section = "CosmosDb";
        
        public string ConnectionString { get; set; }
    }
}