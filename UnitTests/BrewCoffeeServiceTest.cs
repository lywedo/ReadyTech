using Microsoft.Extensions.DependencyInjection;
using ReadyTech.Application.Abstractions;
using UnitTests.Fixtures;

namespace UnitTests
{
    public class BrewCoffeeServiceTest : IClassFixture<ServicesFixture>
    {
        private readonly ServicesFixture _servicesFixture;

        public BrewCoffeeServiceTest()
        {
            _servicesFixture = new ServicesFixture();
        }

        [Fact]
        public async Task GetBrewCoffeeAsync()
        {
            var brewCoffeeService = _servicesFixture.ServiceProvider.GetService<IBrewCoffeeService>();
            Assert.NotNull(brewCoffeeService);

            var result = await brewCoffeeService.GetBrewCoffee(CancellationToken.None);

            Assert.Equal("Your piping hot coffee is ready", result.Message);

        }
    }
}