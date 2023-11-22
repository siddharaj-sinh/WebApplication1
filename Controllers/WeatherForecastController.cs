using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace WebApplication1.Controllers
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
        private IConfiguration configuration;
        private readonly UrlOptions _urlOptions;
        public WeatherForecastController(ILogger<WeatherForecastController> logger, IConfiguration configuration, IOptions<UrlOptions> urlOptions)
        {
            _logger = logger;
            this.configuration = configuration;
            _urlOptions = urlOptions.Value;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("get-env")]
        public IActionResult GetEnv()
        {
            var t = configuration["Demo:Key1"];
            return Ok(new { demo = t });
        }

        [HttpGet("get-env-options")]
        public IActionResult GetEnvOptions()
        {
            var t = _urlOptions.Key1;
            return Ok(new { demo = t });
        }
    }
}