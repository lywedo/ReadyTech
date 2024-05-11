using ReadyTech.Domain.Models;

namespace ReadyTech.Application.Abstractions
{
    public interface IWeatherRepository
    {
        Task<OpenWeather?> GetWeatherByPosition(float lat, float lon, CancellationToken cancellationToken = default);
    }
}
