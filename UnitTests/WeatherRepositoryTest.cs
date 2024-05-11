using Microsoft.Extensions.DependencyInjection;
using ReadyTech.Application.Abstractions;
using ReadyTech.Domain.Models;
using UnitTests.Fixtures;

namespace UnitTests
{
    public class WeatherRepositoryTest : IClassFixture<ServicesFixture>
    {
        private readonly ServicesFixture _servicesFixture;

        public WeatherRepositoryTest()
        {
            _servicesFixture = new ServicesFixture();
        }

        [Fact]
        public async Task GetBrewCoffeeAsync()
        {
            var weatherRepository = _servicesFixture.ServiceProvider.GetService<IWeatherRepository>();
            Assert.NotNull(weatherRepository);

            var result = await weatherRepository.GetWeatherByPosition(-32, 155, CancellationToken.None);
            Assert.NotNull(result);
            Assert.IsType<OpenWeather>(result);

        }
    }
}