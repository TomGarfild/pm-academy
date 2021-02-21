using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
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
            int count = 6;
            success += await Should_Get_Response_With_Info(httpClient);
            success += await Should_Get_Ok_If_Number_Is_Prime(httpClient);
            success += await Should_Get_NotFound_If_Number_Is_Not_Prime(httpClient);
            success += await Should_Get_BadRequest_If_Arguments_Are_Missing_Or_Wrong(httpClient);
            success += await Should_Get_Ok_If_Correct_Primes_In_Range(httpClient);
            success += await Should_Get_Empty_Primes_List(httpClient);
            Console.WriteLine($"{success}/{count} successful tests");
        }

        public static async Task<int> Should_Get_Response_With_Info(HttpClient httpClient)
        {
            var testName = "Get Info";
            Console.WriteLine($"Test: {testName}");

            var request = "";
            Console.WriteLine($"Request: get {httpClient.BaseAddress}{request}");

            var expected = "Primes app by Oleksii Safroniuk";
            var response = await httpClient.GetAsync(httpClient.BaseAddress + request);
            var content = await response.Content.ReadAsStringAsync();
            
            Console.WriteLine($"Expected infor: {expected}");
            Console.WriteLine($"Actual info: {content}");

            Console.WriteLine();
            if (content == expected)
            {
                Console.WriteLine($"Test {testName} accepted\n");
                return 1;
            }

            Console.WriteLine($"Test {testName} failed\n");
            return 0;
        }

        public static async Task<int> Should_Get_Ok_If_Number_Is_Prime(HttpClient httpClient)
        {
            var testName = "Number Is Prime";
            Console.WriteLine($"Test: {testName}");

            var request = "primes/29";
            Console.WriteLine($"Request: get {httpClient.BaseAddress}{request}");

            var expected = (int) HttpStatusCode.OK;
            var response = await httpClient.GetAsync(httpClient.BaseAddress+request);
            var actual = (int)response.StatusCode;

            Console.WriteLine($"Expected status code: {expected}");
            Console.WriteLine($"Actual status code: {actual}");

            Console.WriteLine();
            if (actual == expected)
            {
                Console.WriteLine($"Test {testName} accepted\n");
                return 1;
            }

            Console.WriteLine($"Test {testName} failed\n");
            return 0;
        }

        public static async Task<int> Should_Get_NotFound_If_Number_Is_Not_Prime(HttpClient httpClient)
        {
            var testName = "Number Is Not Prime";
            Console.WriteLine($"Test: {testName}");

            var request = "primes/10";
            Console.WriteLine($"Request: get {httpClient.BaseAddress}{request}");

            var expected = (int)HttpStatusCode.NotFound;
            var response = await httpClient.GetAsync(httpClient.BaseAddress + request);
            var actual = (int)response.StatusCode;

            Console.WriteLine($"Expected status code: {expected}");
            Console.WriteLine($"Actual status code: {actual}");

            Console.WriteLine();
            if (actual == expected)
            {
                Console.WriteLine($"Test {testName} accepted\n");
                return 1;
            }

            Console.WriteLine($"Test {testName} failed\n");
            return 0;
        }

        public static async Task<int> Should_Get_Ok_If_Correct_Primes_In_Range(HttpClient httpClient)
        {
            var testName = "Primes In Range";
            Console.WriteLine($"Test: {testName}");

            var request = "primes/?from=0&to=5";
            Console.WriteLine($"Request: get {httpClient.BaseAddress}{request}");

            var expected = (int)HttpStatusCode.OK;
            var response = await httpClient.GetAsync(httpClient.BaseAddress + request);
            var actual = (int)response.StatusCode;

            var expectedPrimes = new List<int>(){2,3,5};
            var expectedPrimesString = "[" + string.Join(',', expectedPrimes) + "]";
            var primes = await response.Content.ReadAsStringAsync();


            Console.WriteLine($"Expected primes: {expectedPrimesString}");
            Console.WriteLine($"Actual primes: {primes}");

            Console.WriteLine($"Expected status code: {expected}");
            Console.WriteLine($"Actual status code: {actual}");


            Console.WriteLine();
            if (actual != expected && primes == expectedPrimesString)
            {
                Console.WriteLine($"Test {testName} failed\n");
                return 0;
            }
            Console.WriteLine($"Test {testName} accepted\n");
            return 1;
        }

        public static async Task<int> Should_Get_Empty_Primes_List(HttpClient httpClient)
        {
            var testName = "Empty Primes List";
            Console.WriteLine($"Test: {testName}");

            var request = "primes/?from=-5&to=1";
            Console.WriteLine($"Request: get {httpClient.BaseAddress}{request}");

            var expected = (int)HttpStatusCode.OK;
            var response = await httpClient.GetAsync(httpClient.BaseAddress + request);
            var actual = (int)response.StatusCode;

            var primes = await response.Content.ReadAsStringAsync();


            Console.WriteLine($"Expected primes: {"[]"}");
            Console.WriteLine($"Actual primes: {primes}");

            Console.WriteLine($"Expected status code: {expected}");
            Console.WriteLine($"Actual status code: {actual}");


            Console.WriteLine();
            if (actual != expected && primes == "[]")
            {
                Console.WriteLine($"Test {testName} failed\n");
                return 0;
            }
            Console.WriteLine($"Test {testName} accepted\n");
            return 1;
        }

        public static async Task<int> Should_Get_BadRequest_If_Arguments_Are_Missing_Or_Wrong(HttpClient httpClient)
        {
            var testName = "Arguments Are Missing";
            Console.WriteLine($"Test: {testName}");

            var requests = new[] { "primes", "primes?to=10", "primes?from=12", "primes?from=abcde&to=12" };
            foreach (var request in requests)
            {
                Console.WriteLine($"Request: get {httpClient.BaseAddress}{request}");

                var expected = (int)HttpStatusCode.BadRequest;
                var response = await httpClient.GetAsync(httpClient.BaseAddress + request);
                var actual = (int)response.StatusCode;

                Console.WriteLine($"Expected status code: {expected}");
                Console.WriteLine($"Actual status code: {actual}");

                Console.WriteLine();
                if (actual != expected)
                {
                    Console.WriteLine($"Test {testName} failed\n");
                    return 0;
                }
            }
            Console.WriteLine($"Test {testName} accepted\n");
            return 1;
        }

        private static HttpClient GetHttpClient(string path)
        {
            var json = File.ReadAllText(path);
            var settings = JsonSerializer.Deserialize<Settings>(json);
            return new HttpClient() {BaseAddress = new Uri(settings.BaseAddress)};
        }
    }
}
