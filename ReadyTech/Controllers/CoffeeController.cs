using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReadyTech.Application.Abstractions;
using ReadyTech.Application.Dtos;

namespace ReadyTech.Controllers
{
    [ApiController]
    [Route("")]
    public class CoffeeController : ControllerBase
    {

        private readonly ILogger<CoffeeController> _logger;
        private readonly IBrewCoffeeService _brewCoffeeService;
        private readonly ICheckCoffeeStateContext _checkCoffeeStateContext;

        public CoffeeController(ILogger<CoffeeController> logger, IBrewCoffeeService brewCoffeeService, ICheckCoffeeStateContext checkCoffeeStateContext)
        {
            _logger = logger;
            _brewCoffeeService = brewCoffeeService;
            _checkCoffeeStateContext = checkCoffeeStateContext;
        }

        [HttpGet]
        [Route("brew-coffee")]
        public async Task<IActionResult> GetBrewCoffee(CancellationToken cancellationToken)
        {
            var state = await _checkCoffeeStateContext.CheckCoffeeState(cancellationToken);

            if (state == HttpStatusCode.OK)
            {
                return new OkObjectResult(await _brewCoffeeService.GetBrewCoffee(cancellationToken));
            }
            else
            {
                return await Task.FromResult(StatusCode((int)state));
            }

        }
    }
}
