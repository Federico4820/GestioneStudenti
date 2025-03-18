using Serilog;

namespace GestioneStudenti.Services
{
    public static class LoggerService
    {
        private static Serilog.ILogger _logger;

        public static void ConfigureLogger()
        {
            _logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            Log.Logger = _logger;
        }

        public static void LogInformation(string message)
        {
            _logger?.Information(message);
        }

        public static void LogWarning(string message)
        {
            _logger?.Warning(message);
        }

        public static void LogError(string message, Exception ex = null)
        {
            if (ex != null)
                _logger?.Error(ex, message);
            else
                _logger?.Error(message);
        }

        public static void LogDebug(string message)
        {
            _logger?.Debug(message);
        }

        public static void LogFatal(string message, Exception ex = null)
        {
            if (ex != null)
                _logger?.Fatal(ex, message);
            else
                _logger?.Fatal(message);
        }
    }
}

