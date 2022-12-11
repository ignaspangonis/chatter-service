using Microsoft.AspNetCore.Mvc;
using WeatherService.Providers;
using WeatherService.Entities;

namespace ChatterService.Controllers;

[ApiController]
[Route("weather")]
public class WeatherController : ControllerBase
{
    private readonly ILogger<WeatherController> _logger;
    private readonly IWeatherProvider weatherProvider;

    public WeatherController(ILogger<WeatherController> logger)
    {
        _logger = logger;
        weatherProvider = new WeatherProvider();
    }

    [HttpGet(Name = "GetWeather")]
    [ResponseCache(VaryByHeader = "User-Agent", Duration = 300)]
    public async Task<WeatherDto> Get()
    {
        _logger.Log(LogLevel.Information, "GET /weather called");
        return await weatherProvider.GetWeather();
    }
}

