using System.Text.Json;
using Microsoft.Extensions.Options;
using ReadyTech.Application.Abstractions;
using ReadyTech.Domain.Models;

namespace ReadyTech.Infrastructure.Repositories
{
    public class WeatherRepository : IWeatherRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly WeatherSettings _weatherSettings;

        public WeatherRepository(IHttpClientFactory httpClientFactory, IOptions<WeatherSettings> weatherSettings) 
        {
            _httpClientFactory = httpClientFactory;
            _weatherSettings = weatherSettings.Value;
        }

        public async Task<OpenWeather?> GetWeatherByPosition(float lat, float lon, CancellationToken cancellationToken = default)
        {
            string? apiUrl = _weatherSettings.ApiUrl;
            string? apiKey = _weatherSettings.ApiKey;

            if (string.IsNullOrEmpty(apiKey) || string.IsNullOrEmpty(apiUrl))
            {
                throw new ArgumentException("Weather Configration err");
            }

            HttpClient client = _httpClientFactory.CreateClient(nameof(_httpClientFactory));
            OpenWeather? weather;

            try
            {
                HttpResponseMessage response = await client.GetAsync($"{apiUrl}?lat={lat}&lon={lon}&appid={apiKey}", cancellationToken);
                var content = await response.Content.ReadAsStringAsync(cancellationToken);
                weather = JsonSerializer.Deserialize<OpenWeather>(content);
            }
            finally
            {
                client.Dispose();
            }
            
            return weather;
        }
    }
}
