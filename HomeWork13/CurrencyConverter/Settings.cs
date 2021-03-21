using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CurrencyConverter
{
    class Settings
    {
        [JsonPropertyName("baseAddress")]
        public string BaseAddress { get; set; }
    }
}
