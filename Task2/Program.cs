using System;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Task2
{
    public class Program
    {
        public static async Task Main()
        {
            var httpClient = GetHttpClient("settings.json");
            int success = 0;
            success += (await Test1(httpClient));

            Console.WriteLine($"{success} successful tests");
        }

        public static async Task<int> Test1(HttpClient httpClient)
        {
            Console.WriteLine($"Sending request to {httpClient.BaseAddress}");
            var response = (await httpClient.GetAsync(httpClient.BaseAddress));
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Got : {content}");
            var expected = "Primes app by Oleksii Safroniuk";
            Console.WriteLine($"Expected : {expected}");
            return expected.Equals(content) ? 1 : 0;
        }
        private static HttpClient GetHttpClient(string path)
        {
            var json = File.ReadAllText(path);
            var settings = JsonSerializer.Deserialize<Settings>(json);
            return new HttpClient() {BaseAddress = new Uri(settings.BaseAddress)};
        }
    }
}
