using System.Text.Json.Serialization;

namespace Task2
{
    public class Settings
    {
        [JsonPropertyName("primesFrom")]
        public int PrimesFrom { get; set; }
        
        [JsonPropertyName("primesTo")]
        public int PrimesTo { get; set; }
    }
}