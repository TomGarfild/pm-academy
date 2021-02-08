using System;
using System.Net.Http;
using System.Threading.Tasks;
using RequestProcessor.App.Logging;
using RequestProcessor.App.Menu;
using RequestProcessor.App.Services.Impl;

namespace RequestProcessor.App
{
    /// <summary>
    /// Entry point.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Entry point.
        /// </summary>
        /// <returns>Returns exit code.</returns>
        private static async Task<int> Main()
        {
            try
            {
                var httpClient = new HttpClient()
                {
                    Timeout = TimeSpan.FromSeconds(10)
                };
                var requestHandler = new RequestHandler(httpClient);
                var responseHandler = new ResponseHandler();
                var logger = new Logger();

                var performer = new RequestPerformer(requestHandler, responseHandler, logger);
                var options = new OptionsSource("options.json");

                var mainMenu = new MainMenu(performer, options, logger);
                return await mainMenu.StartAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Critical unhandled exception");
                Console.WriteLine(ex);
                return -1;
            }
        }
    }
}
