using System;
using System.Linq;
using System.Threading.Tasks;
using DepsWebApp.Clients;
using DepsWebApp.Models;
using DepsWebApp.Options;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace DepsWebApp.Services
{
    /// <summary>
    /// <see cref="IRatesService"/>
    /// </summary>
    public class RatesService : IRatesService
    {
        private const string RatesCacheKey = "rates";
        private static readonly TimeSpan DefaultCacheLifeTime = TimeSpan.FromHours(1d);

        private readonly IRatesProviderClient _client;
        private readonly IMemoryCache _cache;

        private readonly string _baseCurrency;
        private readonly CacheOptions _cacheOptions;

        /// <summary>
        /// Constructor with DI.
        /// </summary>
        /// <param name="client"><see cref="IRatesProviderClient"/></param>
        /// <param name="cache"><see cref="IMemoryCache"/></param>
        /// <param name="ratesOptions"><see cref="IOptions{RatesOptions}"/></param>
        /// <param name="cacheOptions"><see cref="IOptions{CacheOptions}"/></param>
        public RatesService(
            IRatesProviderClient client, 
            IMemoryCache cache,
            IOptions<RatesOptions> ratesOptions, 
            IOptions<CacheOptions> cacheOptions)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));

            var ratesOptionsValue = ratesOptions?.Value;
            if (ratesOptionsValue == null || !ratesOptionsValue.IsValid)
                throw new ArgumentOutOfRangeException(nameof(ratesOptions), "Options are invalid");
            _baseCurrency = ratesOptionsValue.BaseCurrency.ToUpperInvariant();

            _cacheOptions = cacheOptions?.Value ??
                throw new ArgumentOutOfRangeException(nameof(cacheOptions), "Options are invalid");
        }

        /// <summary>
        /// Exchanges given amount from source currency to destination currency.
        /// </summary>
        /// <param name="srcCurrency">Source currency.</param>
        /// <param name="destCurrency">Destination currency.</param>
        /// <param name="amount">Amount of funds.</param>
        /// <returns><see cref="ExchangeResult"/></returns>
        public async Task<ExchangeResult?> ExchangeAsync(string srcCurrency, string destCurrency, decimal amount)
        {
            var comparer = StringComparer.Ordinal;

            if (string.IsNullOrWhiteSpace(srcCurrency) || 
                string.IsNullOrWhiteSpace(destCurrency)) return null;

            srcCurrency = srcCurrency.ToUpperInvariant();
            destCurrency = destCurrency.ToUpperInvariant();

            // check case with same src. and dest. currencies
            if (comparer.Equals(srcCurrency, destCurrency))
                return new ExchangeResult(decimal.One, amount, amount);
            
            var rates = await GetRatesAsync();
            if (rates == null) throw new InvalidOperationException("Currency rates are invalid");
            
            var srcRate = comparer.Equals(srcCurrency, _baseCurrency) 
                ? decimal.One
                : rates.FirstOrDefault(r => comparer.Equals(r.Currency, srcCurrency))?.Rate;

            var destRate = comparer.Equals(destCurrency, _baseCurrency)
                ? decimal.One
                : rates.FirstOrDefault(r => comparer.Equals(r.Currency, destCurrency))?.Rate;

            if (!srcRate.HasValue || !destRate.HasValue) return null;
            
            var rate = srcRate.Value / destRate.Value;
            return new ExchangeResult(rate, amount, rate * amount);
        }

        private async Task<CurrencyRate[]> GetRatesAsync()
        {
            return await _cache.GetOrCreateAsync(RatesCacheKey, async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow =
                    _cacheOptions.RatesCacheLifeTime ?? DefaultCacheLifeTime;

                var rates = await _client.GetRatesAsync();
                return rates is CurrencyRate[] ratesArray 
                    ? ratesArray 
                    : rates?.ToArray();
            });
        }

        /// <summary>
        /// Actualize rates.
        /// </summary>
        /// <returns>Returns awaiter.</returns>
        public Task ActualizeRatesAsync()
        {
            _cache.Remove(RatesCacheKey);
            return Task.CompletedTask;
        }
    }
}
