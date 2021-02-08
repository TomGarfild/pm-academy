using System;
using System.Net.Http;
using System.Threading.Tasks;
using RequestProcessor.App.Mappings;
using RequestProcessor.App.Models;
using RequestProcessor.App.Models.Impl;

namespace RequestProcessor.App.Services.Impl
{
    internal class RequestHandler : IRequestHandler
    {
        private readonly HttpClient _httpClient;
        public RequestHandler(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IResponse> HandleRequestAsync(IRequestOptions requestOptions)
        {
            using var request = new HttpRequestMessage(
                HttpMethodMappings.Map(requestOptions.Method),
                new Uri(requestOptions.Address));

            using var response = await _httpClient.SendAsync(request);

            return new Response(
                (int)response.StatusCode,
                await response.Content.ReadAsStringAsync());
        }
    }
}