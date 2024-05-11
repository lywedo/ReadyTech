using System.Globalization;
using ReadyTech.Application.Abstractions;
using ReadyTech.Application.Dtos;
using ReadyTech.Helpers;

namespace ReadyTech.Application.Services
{
    public class BrewCoffeeService : IBrewCoffeeService
    {
        private readonly IWeatherRepository _weatherRepository;
        public BrewCoffeeService(IWeatherRepository weatherRepository)
        {
            _weatherRepository = weatherRepository;
        }

        public async Task<BrewCoffeeDto> GetBrewCoffee(CancellationToken cancellationToken = default)
        {
            var result = new BrewCoffeeDto
            {
                Message = $"Your piping hot coffee is ready",
                Prepared = TimeHelper.UtcNow().ToString("o", CultureInfo.InvariantCulture)
            };

            var weather = await _weatherRepository.GetWeatherByPosition(-20, 174, cancellationToken);

            if (weather != null && weather.Current != null)
            {
                var temp = TemperatureHelper.KelvinToCelsius(weather.Current.Temp);
                if (temp > 30) result.Message = "Your refreshing iced coffee is ready";
            }

            return await Task.FromResult(result);
        }


    }
}
