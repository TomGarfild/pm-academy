using DepsWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DepsWebApp.Filters
{
    /// <summary>
    /// Exception filter.
    /// </summary>
    public class ExceptionFilter : IExceptionFilter
    {
        /// <summary>
        /// Calling when exception is thrown.
        /// </summary>
        /// <param name="context">ExceptionContext</param>
        public void OnException(ExceptionContext context)
        {
            var errorCode = context.Exception.GetType().Name switch
            {
                "NullReferenceException" => 601,
                "ArgumentException" => 602,
                "NotImplementedException" => 603,
                _ => 704
            };

            context.Result = new JsonResult(new Error(errorCode, context.Exception.Message));
        }
    }
}