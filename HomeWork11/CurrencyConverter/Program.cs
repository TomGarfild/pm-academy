using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DepsWebApp.Models;

namespace CurrencyConverter
{
    class Program
    {
        public static async Task Main()
        {
            var httpClient = GetHttpClient("settings.json");
            int success = 0;
            int count = 4;
            success += await Should_Return_BadRequest_If_Currency_Does_Not_Exist(httpClient);
            success += await Should_Return_Result_Amount_If_Amount_Has_Value(httpClient);
            success += await Should_Return_Result_Amount_If_Amount_Is_Empty(httpClient);
            success += await Should_Return_Error_When_Register(httpClient);
            Console.WriteLine($"{success}/{count} successful tests");
        }

        public static async Task<int> Should_Return_BadRequest_If_Currency_Does_Not_Exist(HttpClient httpClient)
        {
            var testName = "Incorrect currencies";
            Console.WriteLine($"Test: {testName}");

            var request = $"Rates/UHH/UAH";
            Console.WriteLine($"Request: get {httpClient.BaseAddress}{request}");

            var expectedCode = 400;
            var response = await httpClient.GetAsync(httpClient.BaseAddress + request);
            var actualCode = (int)response.StatusCode;

            return GetResult(actualCode == expectedCode, testName);
        }

        public static async Task<int> Should_Return_Result_Amount_If_Amount_Has_Value(HttpClient httpClient)
        {
            var testName = "Correct currencies";
            Console.WriteLine($"Test: {testName}");

            var amount = 1000m;
            var request = $"Rates/UAH/UAH?amount={amount}";
            Console.WriteLine($"Request: get {httpClient.BaseAddress}{request}");

            var expectedCode = 200;
            var expected = amount;
            var response = await httpClient.GetAsync(httpClient.BaseAddress + request);

            var actualCode = (int)response.StatusCode;
            var actual = decimal.Parse(await response.Content.ReadAsStringAsync());

            return GetResult(expectedCode == actualCode && expected == actual, testName);
        }

        public static async Task<int> Should_Return_Result_Amount_If_Amount_Is_Empty(HttpClient httpClient)
        {
            var testName = "Correct currencies without amount";
            Console.WriteLine($"Test: {testName}");

            var request = "Rates/UAH/UAH";
            Console.WriteLine($"Request: get {httpClient.BaseAddress}{request}");

            var expectedCode = 200;
            var expected = 1m;
            var response = await httpClient.GetAsync(httpClient.BaseAddress + request);

            var actualCode = (int)response.StatusCode;
            var actual = decimal.Parse(await response.Content.ReadAsStringAsync());

            return GetResult(expectedCode == actualCode && expected == actual, testName);
        }

        public static async Task<int> Should_Return_Error_When_Register(HttpClient httpClient)
        {
            var testName = "Register";
            Console.WriteLine($"Test: {testName}");

            var request = "Auth/register";
            Console.WriteLine($"Request: post {httpClient.BaseAddress}{request}");

            var expectedCode = 603;
            var account = new Account()
            {
                Login = "string",
                Password = "string"
            };
            var response = await httpClient.PostAsync(httpClient.BaseAddress + request,
                new StringContent(JsonSerializer.Serialize(account), Encoding.UTF8, "application/json"));

            var responseJson = await response.Content.ReadAsStringAsync();
            var error = JsonSerializer.Deserialize<Error>(responseJson);
            var actualCode = error.Code;

            return GetResult(actualCode == expectedCode, testName);
        }

        private static int GetResult(bool condition, string testName)
        {
            if (condition)
            {
                Console.WriteLine($"Test {testName} accepted\n");
                return 1;
            }

            Console.WriteLine($"Test {testName} failed\n");
            return 0;
        }

        private static HttpClient GetHttpClient(string path)
        {
            var json = File.ReadAllText(path);
            var settings = JsonSerializer.Deserialize<Settings>(json);
            return new HttpClient() { BaseAddress = new Uri(settings.BaseAddress) };
        }
    }
}
