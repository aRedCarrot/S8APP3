namespace APP3_FredFanPage.Services
{
    public interface ILoggerClient
    {
        void LogTrace(string message);
        void LogDebug(string message);
        void LogInformation(string message);
        void LogWarning(string message);
        void LogError(string message);
        void LogCritical(string message);
        void LogConsoleOnly(string message);

    }
}
