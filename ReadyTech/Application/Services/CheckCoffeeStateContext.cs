using System.Net;
using Microsoft.AspNetCore.Http;
using ReadyTech.Application.Abstractions;
using ReadyTech.Extension;
using ReadyTech.Helpers;

namespace ReadyTech.Application.Services
{
    public class CheckCoffeeStateContext : ICheckCoffeeStateContext
    {
        private int callCount = 0;

        public Task<HttpStatusCode> CheckCoffeeState(CancellationToken cancellationToken)
        {
            if (IsAprilFirstDay())
            {
                return Task.FromResult(HttpStatusCodeExtension.ImATeapot);
            }
            else if (IsCoffeeUnavailable())
            {
                return Task.FromResult(HttpStatusCode.ServiceUnavailable);
            }
            else
            {
                return Task.FromResult(HttpStatusCode.OK);
            }
        }

        private bool IsCoffeeUnavailable()
        {
            callCount++;
            return callCount % 5 == 0;
        }

        private bool IsAprilFirstDay()
        {
            return TimeHelper.UtcNow().Month == 4 && TimeHelper.UtcNow().Day == 1;
        }
    }
}
