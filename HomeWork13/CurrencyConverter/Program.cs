using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace CurrencyConverter
{
    class Program
    {
        public static async Task Main()
        {
            var httpClient = GetHttpClient("settings.json");
            var tests = new Tests(httpClient);
            await tests.RunAll();
        }

        private static HttpClient GetHttpClient(string path)
        {
            var json = File.ReadAllText(path);
            var settings = JsonSerializer.Deserialize<Settings>(json);
            return new HttpClient() { BaseAddress = new Uri(settings.BaseAddress) };
        }
    }
}