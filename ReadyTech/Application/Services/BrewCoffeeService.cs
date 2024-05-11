using System.Globalization;
using ReadyTech.Application.Abstractions;
using ReadyTech.Application.Dtos;
using ReadyTech.Helpers;

namespace ReadyTech.Application.Services
{
    public class BrewCoffeeService : IBrewCoffeeService
    {
        public async Task<BrewCoffeeDto> GetBrewCoffee(CancellationToken cancellationToken = default)
        {
            var result = new BrewCoffeeDto
            {
                Message = $"Your piping hot coffee is ready",
                Prepared = TimeHelper.UtcNow().ToString("o", CultureInfo.InvariantCulture)
            };
            
            return await Task.FromResult(result);
        }


    }
}
