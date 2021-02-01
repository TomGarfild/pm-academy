using System.Text.Json.Serialization;

namespace Task3
{
    public class Result
    {
        [JsonPropertyName("success")]
        public int Success { get; }

        [JsonPropertyName("failure")]
        public int Failure { get; }

        public Result(int success, int failure)
        {
            Success = success;
            Failure = failure;
        }
    }
}