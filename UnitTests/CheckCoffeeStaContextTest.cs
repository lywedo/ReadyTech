using System.Net;
using Microsoft.Extensions.DependencyInjection;
using ReadyTech.Application.Abstractions;
using ReadyTech.Extension;
using ReadyTech.Helpers;
using UnitTests.Fixtures;

namespace UnitTests
{
    public class CheckCoffeeStaContextTest : IClassFixture<ServicesFixture>
    {
        private readonly ServicesFixture _servicesFixture;

        public CheckCoffeeStaContextTest()
        {
            _servicesFixture = new ServicesFixture();
        }

        [Fact]
        public async Task GetBrewCoffeeAsync()
        {
            var checkCoffeeStateContext = _servicesFixture.ServiceProvider.GetService<ICheckCoffeeStateContext>();
            Assert.NotNull(checkCoffeeStateContext);

            var round = new Random().Next(20, 51);

            for (var i = 1; i < round; i++)
            {
                var statusCode = await checkCoffeeStateContext.CheckCoffeeState(CancellationToken.None);
                
                if (i % 5 == 0)
                {
                    Assert.Equal(HttpStatusCode.ServiceUnavailable, statusCode);
                }
                else
                {
                    Assert.Equal(HttpStatusCode.OK, statusCode);
                }
            }

            TimeHelper.SetUtcNow(new DateTime(DateTime.UtcNow.Year, 4, 1, 0, 0, 0, DateTimeKind.Utc));

            var statusCode418 = await checkCoffeeStateContext.CheckCoffeeState(CancellationToken.None);
            Assert.Equal(HttpStatusCodeExtension.ImATeapot, statusCode418);
        }
    }
}