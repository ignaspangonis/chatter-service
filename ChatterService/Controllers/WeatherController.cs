using Microsoft.AspNetCore.Mvc;
using WeatherClient.Providers;

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
    public async Task<IActionResult> Get()
    {
        logger.Log(LogLevel.Information, "GET /weather called");

        try
        {
            return Ok(await weatherProvider.GetWeather());
        }
        catch (Exception exception)
        {
            logger.Log(LogLevel.Error, "Error", exception);
            return StatusCode(500);
        }
    }
}

