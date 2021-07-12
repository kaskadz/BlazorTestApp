using System.Threading.Tasks;
using BlazorTestApp.Model;

namespace BlazorTestApp.Frontend.Clients
{
    public interface IWebApiClient
    {
        public Task<WeatherForecast[]> GetForecasts();
    }
}