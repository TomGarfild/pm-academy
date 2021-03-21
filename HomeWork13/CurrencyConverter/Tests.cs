using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DepsWebApp.Models;

namespace CurrencyConverter
{
    public class Tests
    {
        private readonly HttpClient _httpClient;
        public Tests(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task RunAll()
        {
            int success = 0;
            int count = 6;
            success += await Should_Return_BadRequest_When_UnAuthorized(_httpClient);
            var login = "login45";
            var password = "123456qewrtt";
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(
                    "Basic", 
                    Convert.ToBase64String(Encoding.UTF8.GetBytes($"{login}:{password}")));
            var content = new StringContent(JsonSerializer.Serialize(new User(login, password)), Encoding.UTF8, "application/json");
            success += await Should_Return_Ok_When_Register(_httpClient, content);
            success += await Should_Return_BadRequest_If_Currency_Does_Not_Exist(_httpClient);
            success += await Should_Return_Result_Amount_If_Amount_Has_Value(_httpClient);
            success += await Should_Return_Result_Amount_If_Amount_Is_Empty(_httpClient);
            password = "1234";
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(
                    "Basic",
                    Convert.ToBase64String(Encoding.UTF8.GetBytes($"{login}:{password}")));
            success += await Should_Return_UnAuthorized_When_Wrong_Password(_httpClient);
            Console.WriteLine($"{success}/{count} successful tests");
        }


        private async Task<int> Should_Return_BadRequest_When_UnAuthorized(HttpClient httpClient)
        {
            var testName = "Unauthorized";
            Console.WriteLine($"Test: {testName}");
            var request = $"Rates/UHH/UAH";
            Console.WriteLine($"Request: get {httpClient.BaseAddress}{request}");
            var expectedCode = 401;
            var response = await httpClient.GetAsync(httpClient.BaseAddress + request);
            var actualCode = (int)response.StatusCode;

            return GetResult(actualCode == expectedCode, testName);
        }

        private async Task<int> Should_Return_Ok_When_Register(HttpClient httpClient, StringContent user)
        {
            var testName = "Register";
            Console.WriteLine($"Test: {testName}");
            var request = $"Auth/register";
            Console.WriteLine($"Request: get {httpClient.BaseAddress}{request}");
            var expectedCode = 200;
            var response = await httpClient.PostAsync(httpClient.BaseAddress + request, user);
            var actualCode = (int)response.StatusCode;

            return GetResult(actualCode == expectedCode, testName);
        }

        private async Task<int> Should_Return_BadRequest_If_Currency_Does_Not_Exist(HttpClient httpClient)
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

        private async Task<int> Should_Return_Result_Amount_If_Amount_Has_Value(HttpClient httpClient)
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

        private async Task<int> Should_Return_Result_Amount_If_Amount_Is_Empty(HttpClient httpClient)
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

        private async Task<int> Should_Return_UnAuthorized_When_Wrong_Password(HttpClient httpClient)
        {
            var testName = "Wrong password";
            Console.WriteLine($"Test: {testName}");

            var request = "Rates/UAH/UAH";
            Console.WriteLine($"Request: get {httpClient.BaseAddress}{request}");

            var expectedCode = 401;
            var response = await httpClient.GetAsync(httpClient.BaseAddress + request);

            var actualCode = (int)response.StatusCode;


            return GetResult(expectedCode == actualCode, testName);
        }
        

        private int GetResult(bool condition, string testName)
        {
            if (condition)
            {
                Console.WriteLine($"Test {testName} accepted\n");
                return 1;
            }

            Console.WriteLine($"Test {testName} failed\n");
            return 0;
        }
    }
}