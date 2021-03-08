using System;
using System.IO;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.IO;

namespace DepsWebApp.Middlewares
{
    /// <summary>
    /// Middleware for logging.
    /// </summary>
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggingMiddleware> _logger;
        private readonly RecyclableMemoryStreamManager _recyclableMemoryStreamManager;

        /// <summary>
        /// Constructor for logging middleware.
        /// </summary>
        /// <param name="next">Next component in the pipeline.</param>
        /// <param name="logger">For logging information.</param>
        public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
            _recyclableMemoryStreamManager = new RecyclableMemoryStreamManager();
        }

        /// <summary>
        /// Logging requests and responses.
        /// </summary>
        /// <param name="context">HttpContext</param>
        /// <returns>Returns task.</returns>
        public async Task InvokeAsync(HttpContext context)
        {
            var requestBody = await ObtainRequestBody(context.Request);
            _logger.LogInformation(requestBody);

            var originalBodyStream = context.Response.Body;
            await using var responseBodyStream = _recyclableMemoryStreamManager.GetStream();
            context.Response.Body = responseBodyStream;

            await _next.Invoke(context);

            var responseBody = await ObtainResponseBody(context);
            await responseBodyStream.CopyToAsync(originalBodyStream);
            _logger.LogInformation(responseBody);
        }

        private static async Task<string> ObtainRequestBody(HttpRequest request)
        {
            if (request.Body == null) return string.Empty;

            request.EnableBuffering();
            var encoding = GetEncodingFromContentType(request.ContentType);
            string bodyStr;
            using (var reader = new StreamReader(request.Body, encoding, true, 1024, true))
            {
                bodyStr = await reader.ReadToEndAsync().ConfigureAwait(false);
            }
            request.Body.Seek(0, SeekOrigin.Begin);

            return bodyStr;
        }

        private static async Task<string> ObtainResponseBody(HttpContext context)
        {
            var response = context.Response;
            response.Body.Seek(0, SeekOrigin.Begin);
            var encoding = GetEncodingFromContentType(response.ContentType);
            using var reader = new StreamReader(response.Body, encoding,
                false, 4096, true);
            var text = await reader.ReadToEndAsync().ConfigureAwait(false);
            response.Body.Seek(0, SeekOrigin.Begin);

            return text;
        }

        private static Encoding GetEncodingFromContentType(string contentTypeStr)
        {
            if (string.IsNullOrEmpty(contentTypeStr))
            {
                return Encoding.UTF8;
            }

            ContentType contentType;

            try
            {
                contentType = new ContentType(contentTypeStr);
            }
            catch (FormatException)
            {
                return Encoding.UTF8;
            }

            if (string.IsNullOrEmpty(contentType.CharSet))
            {
                return Encoding.UTF8;
            }

            return Encoding.GetEncoding(contentType.CharSet, EncoderFallback.ExceptionFallback, DecoderFallback.ExceptionFallback);
        }
    }
}
