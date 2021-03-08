using System.Text.Json.Serialization;

namespace CurrencyConverter
{
    public class Settings
    {
        [JsonPropertyName("baseAddress")]
        public string BaseAddress { get; set; }
    }
}