using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using RequestProcessor.App.Exceptions;
using RequestProcessor.App.Models;
using RequestProcessor.App.Models.Impl;

namespace RequestProcessor.App.Services.Impl
{
    internal class OptionsSource : IOptionsSource
    {
        private readonly string _optionsPath;

        public OptionsSource(string path)
        {
            _optionsPath = path;
        }

        public async Task<IEnumerable<(IRequestOptions, IResponseOptions)>> GetOptionsAsync()
        {
            try
            {
                var json = await File.ReadAllTextAsync(_optionsPath);
                var jsonOptions = new JsonSerializerOptions(){
                    Converters =
                    {
                        new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
                    }
                };
                
                var options = JsonSerializer.Deserialize<List<RequestOptions>>(json, jsonOptions);
                return options.Select(opt => ((IRequestOptions)opt, (IResponseOptions)opt));
            }
            catch (Exception exception)
            {
                throw new PerformException("Something went wrong, while getting options.", exception);
            }
            
        }
    }
}