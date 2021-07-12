using System.Net.Http;

namespace BlazorTestApp.Frontend.Clients
{
    public class StaticContentClient : IStaticContentClient
    {
        private readonly HttpClient _httpClient;

        public StaticContentClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
    }
}