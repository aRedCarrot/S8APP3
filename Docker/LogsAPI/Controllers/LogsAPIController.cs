using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LogsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LogsAPIController : ControllerBase
    {
        private readonly string logsFile = "logs.txt";

        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<LogsAPIController> _logger;

        public LogsAPIController(ILogger<LogsAPIController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "Getlogs")]
        public IActionResult Get()
        {
            return Ok(Enumerable.Range(1, 5).Select(index => new Logs
            {
                Date = DateTime.Now.AddDays(index),
                Type = 1,
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray());
        }

        [HttpPost(Name = "Postlog")]
        public IActionResult Post(Logs log)
        {

            StreamWriter r = new StreamWriter(logsFile, true);
            var json = JsonConvert.SerializeObject(log);
            r.WriteLine(json);
            r.Close();

            return Ok("Done");
        }
    }
}