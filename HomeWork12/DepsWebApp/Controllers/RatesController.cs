using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using DepsWebApp.Services;
using Microsoft.AspNetCore.Authorization;

namespace DepsWebApp.Controllers
{
    /// <summary>
    /// Rates controller.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class RatesController : ControllerBase
    {
        private readonly ILogger<RatesController> _logger;
        private readonly IRatesService _rates;

        /// <summary>
        /// Constructor with DI.
        /// </summary>
        /// <param name="rates">Rates.</param>
        /// <param name="logger">Logger.</param>
        public RatesController(
            IRatesService rates,
            ILogger<RatesController> logger)
        {
            _rates = rates;
            _logger = logger;
        }

        /// <summary>
        /// Exchange currencies.
        /// </summary>
        /// <param name="srcCurrency">Source currency.</param>
        /// <param name="dstCurrency">Destination currency.</param>
        /// <param name="amount">Amount.</param>
        /// <returns></returns>
        [HttpGet("{srcCurrency}/{dstCurrency}")]
        public async Task<ActionResult<decimal>> Get(string srcCurrency, string dstCurrency, decimal? amount)
        {
            var exchange =  await _rates.ExchangeAsync(srcCurrency, dstCurrency, amount ?? decimal.One);
            if (!exchange.HasValue)
            {
                _logger.LogDebug($"Can't exchange from '{srcCurrency}' to '{dstCurrency}'");
                return BadRequest("Invalid currency code");
            }
            return exchange.Value.DestinationAmount;
        }
    }
}
