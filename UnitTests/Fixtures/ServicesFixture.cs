using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReadyTech.Application.Abstractions;
using ReadyTech.Application.Services;
using ReadyTech.Domain.Models;
using ReadyTech.Infrastructure.Repositories;

namespace UnitTests.Fixtures
{
    public class ServicesFixture
    {
        public IServiceProvider ServiceProvider { get; set; }

        public ServicesFixture()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            ServiceProvider = new ServiceCollection()
            .AddHttpClient()
                .Configure<WeatherSettings>(configuration.GetSection("WeatherSettings"))
                .AddScoped<IBrewCoffeeService, BrewCoffeeService>()
                .AddSingleton<ICheckCoffeeStateContext, CheckCoffeeStateContext>()
                .AddTransient<IWeatherRepository, WeatherRepository>()
                .BuildServiceProvider();
        }
    }
}
