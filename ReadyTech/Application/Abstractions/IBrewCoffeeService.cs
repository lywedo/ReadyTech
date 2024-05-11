using ReadyTech.Application.Dtos;

namespace ReadyTech.Application.Abstractions
{
    public interface IBrewCoffeeService
    {
        Task<BrewCoffeeDto> GetBrewCoffee(CancellationToken cancellationToken = default);
    }
}
