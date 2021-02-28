using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Task1.Controllers
{
    [ApiController]
    public class StatusController : Controller
    {
        private readonly ILogger<StatusController> _logger;

        public StatusController(ILogger<StatusController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("/")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public IActionResult GetInfo()
        {
            var response = "Primes app by Oleksii Safroniuk";
            _logger.LogInformation($"Writes response: {response}. Status code: {HttpStatusCode.OK}");
            return Ok(response);
        }
    }
}
