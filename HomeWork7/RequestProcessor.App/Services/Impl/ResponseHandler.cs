using System.IO;
using System.Threading.Tasks;
using RequestProcessor.App.Models;

namespace RequestProcessor.App.Services.Impl
{
    internal class ResponseHandler : IResponseHandler
    {
        public Task HandleResponseAsync(IResponse response, IRequestOptions requestOptions, IResponseOptions responseOptions)
        {
            return File.WriteAllTextAsync(responseOptions.Path, 
                $"Status code: {response.Code}\nSuccess: {response.Handled}\nContent: {response.Content}");
        }
    }
}