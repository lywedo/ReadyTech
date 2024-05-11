using System.Net;

namespace ReadyTech.Application.Abstractions
{
    public interface ICheckCoffeeStateContext
    {
        Task<HttpStatusCode> CheckCoffeeState(CancellationToken cancellationToken = default);
    }
}
