using System.Text.Json.Serialization;

namespace Task2
{
    public class Settings
    {
        [JsonPropertyName("baseAddress")]
        public string BaseAddress { get; set; }
    }
}