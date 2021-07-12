using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BlazorTestApp.Model;

namespace BlazorTestApp.Frontend.Clients
{
    public class WebApiClient : IWebApiClient
    {
        private readonly HttpClient _httpClient;

        public WebApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<WeatherForecast[]> GetForecasts()
        {
            return await _httpClient.GetFromJsonAsync<WeatherForecast[]>("WeatherForecast");
        }
    }
}