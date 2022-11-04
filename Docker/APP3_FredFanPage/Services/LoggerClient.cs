using APP3_FredFanPage.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace APP3_FredFanPage.Services
{
    public class LoggerClient : ILoggerClient
    {
        private readonly ILogger<LoggerClient> _serilogLogger;
        private readonly HttpClient httpClient;

        public LoggerClient(
            ILogger<LoggerClient> serilogLogger, 
            IHttpClientFactory httpClientFactory,
            IOptions<LogSettings> settings)
        {
            _serilogLogger = serilogLogger;
            httpClient = httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(settings.Value.URL);
        }

        public void LogConsoleOnly(string message)
        {
            _serilogLogger.LogInformation(message);
        }

        public void LogCritical(string message)
        => Log(LogLevel.Critical, message);

        public void LogDebug(string message)
        => Log(LogLevel.Debug, message);

        public void LogError(string message)
        => Log(LogLevel.Error, message);


        public void LogInformation(string message)
        => Log(LogLevel.Information, message);

        public void LogTrace(string message)
        => Log(LogLevel.Trace, message);

        public void LogWarning(string message)
        => Log(LogLevel.Warning, message);

        private async void Log(LogLevel logLevel, string message)
        {
            _serilogLogger.Log(logLevel, message);

            var jsonContent = new StringContent(
                JsonSerializer.Serialize(
                    new Logs()
                    {
                        Level = (int)logLevel,
                        Summary = message,
                        Date = DateTime.UtcNow
                    }),
                Encoding.UTF8,
                "application/json");

            try
            {

                var response = await httpClient.PostAsync(string.Empty, jsonContent);
                _serilogLogger.LogInformation("Log return code " + response.StatusCode);
            }
            catch (Exception e)
            {
                _serilogLogger.LogError(e, "Log connection faild");
            }
        }
    }
}
