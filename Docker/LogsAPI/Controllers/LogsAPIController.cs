using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Runtime.InteropServices;

namespace LogsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LogsAPIController : ControllerBase
    {

        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<LogsAPIController> _logger;
        private readonly IConfiguration _configuration;

        public LogsAPIController(ILogger<LogsAPIController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet(Name = "Getlogs")]
        public IActionResult Get()
        {
            return Ok(Enumerable.Range(1, 5).Select(index => new Logs
            {
                Date = DateTime.Now.AddDays(index),
                Level = 1,
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray());
        }

        [HttpPost(Name = "Postlog")]
        public IActionResult Post(Logs log)
        {
            if (_configuration.GetValue<int>("Logging:LogLevel") > log.Level)
            {
                var filename = _configuration.GetValue<string>("Logging:Filename").Replace("{Level}", log.Level.ToString());

                StreamWriter r = new StreamWriter(filename, true);
                var json = JsonConvert.SerializeObject(log);
                r.WriteLine(json);
                r.Close();
            }

            return Ok("Done");
        }
    }
}