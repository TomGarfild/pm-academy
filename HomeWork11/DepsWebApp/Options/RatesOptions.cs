namespace DepsWebApp.Options
{
    /// <summary>
    /// Rates options.
    /// </summary>
    public class RatesOptions
    {
        /// <summary>
        /// Base currency.
        /// </summary>
        public string BaseCurrency { get; set; }

        /// <summary>
        /// Is true when <see cref="BaseCurrency"/> is valid.
        /// </summary>
        public bool IsValid => !string.IsNullOrWhiteSpace(BaseCurrency);
    }
}
