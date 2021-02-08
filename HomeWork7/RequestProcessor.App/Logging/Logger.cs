using System;
using System.Diagnostics;

namespace RequestProcessor.App.Logging
{
    internal class Logger : ILogger
    {
        public void Log(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                Debug.WriteLine(message);
            }
        }

        public void Log(Exception exception, string message)
        {
            Log(exception.ToString());
            Log(message);
        }
    }
}