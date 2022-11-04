using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

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
        public IActionResult Get(int level)
        {

            try
            {
                var filename = _configuration.GetValue<string>("Logging:Filename").Replace("{Level}", level.ToString());
                StreamReader r = new StreamReader(filename, true);
                List<Logs> llogs = new List<Logs>();
                String line;
                while ((line = r.ReadLine()) != null)
                {
                    Logs log = JsonConvert.DeserializeObject<Logs>(line);
                    llogs.Add(log);
                }
                r.Close();

                return Ok(llogs);
            }
            catch 
            {
                return NoContent();
            }
        }

        [HttpPost(Name = "Postlog")]
        public IActionResult Post(Logs log)
        {

            if (_configuration.GetValue<int>("Logging:LogLevel") > log.Level)
            {
                try
                { 
                    var filename = _configuration.GetValue<string>("Logging:Filename").Replace("{Level}", log.Level.ToString());

                    StreamWriter r = new StreamWriter(filename, true);
                    var json = JsonConvert.SerializeObject(log);
                    r.WriteLine(json);
                    r.Close();
                }catch
                {
                    return BadRequest();
                }
            }

            return Ok("Done");
        }
    }
}