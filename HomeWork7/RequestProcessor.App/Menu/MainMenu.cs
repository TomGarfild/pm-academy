using System;
using System.Linq;
using System.Threading.Tasks;
using RequestProcessor.App.Exceptions;
using RequestProcessor.App.Logging;
using RequestProcessor.App.Services;

namespace RequestProcessor.App.Menu
{
    /// <summary>
    /// Main menu.
    /// </summary>
    internal class MainMenu : IMainMenu
    {
        private readonly IRequestPerformer _performer;
        private readonly IOptionsSource _options;
        private readonly ILogger _logger;

        /// <summary>
        /// Constructor with DI.
        /// </summary>
        /// <param name="options">Options source</param>
        /// <param name="performer">Request performer.</param>
        /// <param name="logger">Logger implementation.</param>

        public MainMenu(
            IRequestPerformer performer, 
            IOptionsSource options, 
            ILogger logger)
        {
            _performer = performer;
            _options = options;
            _logger = logger;
        }

        public async Task<int> StartAsync()
        {
            WriteLine("Processor of HTTP-requests.");
            WriteLine("Author: Safroniuk Oleksii\n");
            try
            {
                WriteLine("Getting options...");
                _logger.Log("Getting options...");
                var options = (await _options.GetOptionsAsync())
                    .Where(o => o.Item1 != null && o.Item2 != null
                                                && o.Item1.IsValid && o.Item2.IsValid);

                WriteLine("Start tasks...");
                _logger.Log("Start tasks...");
                var tasks = options.Select(opt => _performer.PerformRequestAsync(opt.Item1, opt.Item2)).ToArray();

                WriteLine("Wait for all tasks...");
                _logger.Log("Wait for all tasks...");
                Task.WaitAll(tasks);

                WriteLine("Application did successfully all work.");
                _logger.Log("Application did successfully all work.");
                return 0;
            }
            catch (AggregateException exception)
            {
                foreach (var ex in exception.InnerExceptions)
                {
                    if (ex is PerformException)
                    {
                        WriteLine(ex.Message);
                        _logger.Log(ex, ex.Message);
                    }
                }
                
                return 1;
            }
        }

        public static void Write(string message)
        {
            Console.Write(message);
        }

        public static void WriteLine(string message)
        {
            Console.WriteLine(message);
        }
    }
}
