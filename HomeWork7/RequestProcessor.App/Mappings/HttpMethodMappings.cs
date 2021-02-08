using System;
using System.Net.Http;
using RequestProcessor.App.Models;

namespace RequestProcessor.App.Mappings
{
    internal class HttpMethodMappings
    {
        public static HttpMethod Map(RequestMethod method)
        {
            switch (method)
            {
                case RequestMethod.Get:
                    return HttpMethod.Get;
                case RequestMethod.Post:
                    return HttpMethod.Post;
                case RequestMethod.Put:
                    return HttpMethod.Put;
                case RequestMethod.Patch:
                    return HttpMethod.Patch;
                case RequestMethod.Delete:
                    return HttpMethod.Delete;
                default:
                    throw new ArgumentOutOfRangeException("Request method id undefined.");
            }
        }
    }
}