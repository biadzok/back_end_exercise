using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)],
                DorsetCollege = "We are pushing our changes to GitHub"
            })
            .ToArray();
        }

        [HttpGet("list")]
        public IEnumerable<string> Get_List()
        {
            return Summaries.ToList();
        }

        [HttpGet("list/{id}")]
        public string Get_list_byId(int id)
        {
            if(id < 0 || id > Summaries.Length - 1)
            {
                return new string("not found");
            }
            else
            {
                return Summaries[id];
            }
        }
    }
}
