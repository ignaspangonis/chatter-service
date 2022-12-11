using Microsoft.AspNetCore.Mvc;
using WeatherService.Providers;
using WeatherService.Entities;
using ChatterService.Services;
using Microsoft.Extensions.Logging;

namespace ChatterService.Controllers;

[ApiController]
[Route("weather")]
public class WeatherController : ControllerBase
{
    private readonly ILogger<WeatherController> logger;
    private readonly IWeatherProvider weatherProvider;

    public WeatherController(ILogger<WeatherController> logger)
    {
        this.logger = logger;
        weatherProvider = new WeatherProvider();
    }

    [HttpGet(Name = "GetWeather")]
    [ResponseCache(VaryByHeader = "User-Agent", Duration = 300)]
    public async Task<WeatherDto> Get()
    {
        logger.Log(LogLevel.Information, "GET /weather called");

        try
        {
            return await weatherProvider.GetWeather();
        }
        catch (Exception exception)
        {
            logger.Log(LogLevel.Error, "Error", exception);
        }
    }
}

