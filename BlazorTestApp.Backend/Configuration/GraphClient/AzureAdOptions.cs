namespace BlazorTestApp.Backend.Configuration.GraphClient
{
    public class AzureAdOptions
    {
        public const string Section = "AzureAd";

        public string Instance { get; set; }

        public string Domain { get; set; }

        public string TenantId { get; set; }

        public string ClientId { get; set; }
        
        public string Secret { get; set; }
    }
}