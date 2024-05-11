using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ReadyTech.Application.Abstractions;
using ReadyTech.Application.Dtos;
using ReadyTech.Controllers;
using ReadyTech.Extension;
using ReadyTech.Helpers;
using UnitTests.Fixtures;

namespace IntegrationTests
{
    public class IntegrationControllerTest : IClassFixture<ServicesFixture>
    {
        private readonly ServicesFixture _servicesFixture;

        public IntegrationControllerTest(ServicesFixture servicesFixture)
        {
            _servicesFixture = servicesFixture;
        }

        [Fact]
        public async Task IntegrationTestCoffeeContrller()
        {
            var coffeeService = _servicesFixture.ServiceProvider.GetService<IBrewCoffeeService>();
            var checkStateContext = _servicesFixture.ServiceProvider.GetService<ICheckCoffeeStateContext>();
            
            Assert.NotNull(coffeeService);
            Assert.NotNull(checkStateContext);
            var controller = new CoffeeController(new FackLogger(), coffeeService, checkStateContext);
            Assert.NotNull(controller);

            
            var round = new Random().Next(20, 51);

            for (var i = 1; i < round; i++)
            {
                var result = await controller.GetBrewCoffee(CancellationToken.None);

                if (i % 5 == 0)
                {
                    var serverUnavailable = Assert.IsType<StatusCodeResult>(result);
                    Assert.Equal((int)HttpStatusCode.ServiceUnavailable, serverUnavailable.StatusCode);
                }
                else
                {
                    var ok = Assert.IsType<OkObjectResult>(result);
                    Assert.IsType<BrewCoffeeDto>(ok.Value);
                }
            }

            TimeHelper.SetUtcNow(new DateTime(DateTime.UtcNow.Year, 4, 1, 0, 0, 0, DateTimeKind.Utc));

            var imATeapotResult = await controller.GetBrewCoffee(CancellationToken.None);
            var imATeapot = Assert.IsType<StatusCodeResult>(imATeapotResult);
            Assert.Equal((int)HttpStatusCodeExtension.ImATeapot, imATeapot.StatusCode);
        }

        class FackLogger : ILogger<CoffeeController>
        {
            public IDisposable? BeginScope<TState>(TState state) where TState : notnull
            {
                throw new NotImplementedException();
            }

            public bool IsEnabled(LogLevel logLevel)
            {
                throw new NotImplementedException();
            }

            public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
            {
                throw new NotImplementedException();
            }
        }
    }
}