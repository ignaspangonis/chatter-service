using Microsoft.AspNetCore.Mvc;
using WeatherService.Providers;
using WeatherService.Entities;

namespace ChatterService.Controllers;

[ApiController]
[Route("weather")]
public class WeatherController : ControllerBase
{
    private readonly ILogger<WeatherController> _logger;
    private readonly WeatherProvider weatherProvider;

    public WeatherController(ILogger<WeatherController> logger)
    {
        _logger = logger;
        weatherProvider = new WeatherProvider();
    }

    [HttpGet(Name = "GetWeather")]
    [ResponseCache(VaryByHeader = "User-Agent", Duration = 30)]
    public WeatherDto Get()
    {
        _logger.Log(LogLevel.Information, "GET /weather-new called");
        return weatherProvider.GetWeather();
    }
}

