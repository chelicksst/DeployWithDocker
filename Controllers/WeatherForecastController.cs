using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace DeployWithDocker.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors("AllowClient")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        private readonly IConfiguration _configuration;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IConfiguration configuration)
        {
            _configuration = configuration;
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public string Get()
        {
            return "Freezing and Cool";
        }
    }
}
