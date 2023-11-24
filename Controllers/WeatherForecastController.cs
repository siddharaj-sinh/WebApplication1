using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using WebApplication1.Data;

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
        private readonly JwtConfigurationOptions _jwtOptions;
        private readonly ApplicationDbContext _context;
        public WeatherForecastController(ILogger<WeatherForecastController> logger, IConfiguration configuration, IOptions<UrlOptions> urlOptions, IOptions<JwtConfigurationOptions> jwtOptions, ApplicationDbContext applicationDbContext)
        {
            _logger = logger;
            this.configuration = configuration;
            _urlOptions = urlOptions.Value;
            _jwtOptions = jwtOptions.Value;
            _context = applicationDbContext;
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
        [HttpGet("get-jwt")]
        public IActionResult GetJwtOptions()
        {
            return Ok(new
            {
                key = _jwtOptions.Key,
                issuer = _jwtOptions.Issuer,
                audience = _jwtOptions.Audience,
                expiryInMinutes = _jwtOptions.ExpiryInMinutes,
            });
        }

        [HttpGet("get-azure-appsetting")]
        public IActionResult GetAnonmysOptions()
        {
            return Ok(new
            {
                t = _jwtOptions.NotInLocal
            });
        }
        [HttpGet("get-user")]
        public async Task<IActionResult> GetUser() {

            var result = await _context.User.ToListAsync();

            return Ok( new {user=result});
        }
    }
}