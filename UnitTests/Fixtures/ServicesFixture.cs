using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using ReadyTech.Application.Abstractions;
using ReadyTech.Application.Services;

namespace UnitTests.Fixtures
{
    public class ServicesFixture
    {
        public IServiceProvider ServiceProvider { get; set; }

        public ServicesFixture() 
        {
            ServiceProvider = new ServiceCollection()
                .AddScoped<IBrewCoffeeService, BrewCoffeeService>()
                .AddSingleton<ICheckCoffeeStateContext, CheckCoffeeStateContext>()
                .BuildServiceProvider();
        }
    }
}
