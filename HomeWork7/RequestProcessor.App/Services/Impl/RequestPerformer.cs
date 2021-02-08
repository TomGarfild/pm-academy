using System;
using System.Net.Http;
using System.Threading.Tasks;
using RequestProcessor.App.Exceptions;
using RequestProcessor.App.Logging;
using RequestProcessor.App.Menu;
using RequestProcessor.App.Models;
using RequestProcessor.App.Models.Impl;

namespace RequestProcessor.App.Services.Impl
{
    /// <summary>
    /// Request performer.
    /// </summary>
    internal class RequestPerformer : IRequestPerformer
    {
        private readonly IRequestHandler _requestHandler;
        private readonly IResponseHandler _responseHandler;
        private readonly ILogger _logger;

        /// <summary>
        /// Constructor with DI.
        /// </summary>
        /// <param name="requestHandler">Request handler implementation.</param>
        /// <param name="responseHandler">Response handler implementation.</param>
        /// <param name="logger">Logger implementation.</param>

        public RequestPerformer(
            IRequestHandler requestHandler, 
            IResponseHandler responseHandler,
            ILogger logger)
        {
            _requestHandler = requestHandler;
            _responseHandler = responseHandler;
            _logger = logger;
        }

        /// <inheritdoc/>
        public async Task<bool> PerformRequestAsync(
            IRequestOptions requestOptions, 
            IResponseOptions responseOptions)
        {
            Response response = null;
            try
            {
                MainMenu.WriteLine($"Start sending: {requestOptions.Name}...");
                _logger.Log($"Start sending: {requestOptions.Name}...");
                response = await _requestHandler.HandleRequestAsync(requestOptions) as Response;

                MainMenu.WriteLine($"Status code: {response?.Code} for {requestOptions.Name}");
                _logger.Log($"Status code: {response?.Code} for {requestOptions.Name}");
                return true;
            }
            catch (Exception exception)
            {
                if (response != null)
                    response.Handled = false;
                string message;
                switch (exception)
                {
                    case ArgumentNullException _:
                        message = "Request options is missing.";
                        break;
                    case ArgumentOutOfRangeException _:
                        message = "Request options is not valid.";
                        break;
                    case InvalidOperationException _:
                        message = "Invalid client state.";
                        break;
                    case HttpRequestException _:
                    case TaskCanceledException _:
                        message = "Internal client exception.";
                        break;
                    default:
                        message = "Unexpected error occurred";
                        break;
                }
                throw new PerformException(message, exception);
            }
            finally
            {
                if (response != null)
                    await _responseHandler.HandleResponseAsync(response, requestOptions, responseOptions);
            }
        }
    }
}
