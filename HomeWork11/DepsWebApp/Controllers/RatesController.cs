using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using DepsWebApp.Services;

namespace DepsWebApp.Controllers
{
    /// <summary>
    /// Rates controller.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class RatesController : ControllerBase
    {
        private readonly ILogger<RatesController> _logger;
        private readonly IRatesService _rates;

        /// <summary>
        /// Constructor with DI.
        /// </summary>
        /// <param name="rates"><see cref="IRatesService"/> for exchanging currency.</param>
        /// <param name="logger"><see cref="ILogger"/> for logging information.</param>
        public RatesController(
            IRatesService rates,
            ILogger<RatesController> logger)
        {
            _rates = rates;
            _logger = logger;
        }

        /// <summary>
        /// Exchange currency from source currency to destination currency.
        /// </summary>
        /// <param name="srcCurrency">Source currency.</param>
        /// <param name="dstCurrency">Destination currency.</param>
        /// <param name="amount">Amount for exchange.</param>
        /// <returns>Result amount after exchange.</returns>
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
