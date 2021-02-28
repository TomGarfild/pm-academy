using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Task1.Services;

namespace Task1.Controllers
{
    [ApiController]
    public class PrimesController : Controller
    {
        private readonly ILogger<PrimesController> _logger;
        private readonly IPrimesService _primesService;

        public PrimesController(ILogger<PrimesController> logger, IPrimesService primesService)
        {
            _logger = logger;
            _primesService = primesService;
        }

        [Route("/primes/{number:int}")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> CheckIsNumberPrime()
        {
            var number = int.Parse((string)HttpContext.Request.RouteValues["number"]);
            _logger.LogInformation($"Check is {number} prime.");

            var isPrime = await _primesService.IsPrimeAsync(number);
            if (isPrime)
            {
                _logger.LogInformation($"Status code: {(int)HttpStatusCode.OK}");

                return Ok($"{number} is prime");
            }
            _logger.LogInformation($"Status code: {(int)HttpStatusCode.NotFound}");

            return NotFound($"{number} is not prime");
        }

        [Route("/primes")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetPrimesInRange()
        {
            var fromString = HttpContext.Request.Query["from"].FirstOrDefault();
            var toString   = HttpContext.Request.Query["to"].FirstOrDefault();
            if (int.TryParse(toString, out var to) && int.TryParse(fromString, out var from))
            {
                _logger.LogInformation($"Get primes in range [{from}, {to}]");
                var primes = await _primesService.GetPrimesAsync(from, to);
                _logger.LogInformation($"Got {primes.Count} prime numbers");
                return Ok("[" + string.Join(',', primes) + "]");
            }
            _logger.LogInformation($"Wrong range.");

            return BadRequest("Wrong range");
        }
    }
}