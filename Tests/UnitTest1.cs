namespace Tests;
using WeatherService;
using WeatherService.Interfaces;
using WeatherService.Providers;

public class Tests
{
    IWeatherProvider provider;

    [OneTimeSetUp]
    public void Init()
    {
        var data = new List<WeatherService.WeatherForecast> {
            new WeatherService.WeatherForecast()
        };

        // init fake provider (mock provider)
        provider = new FakeWeatherProvider(data);
    }

    [Test]
    public void Test1()
    {
        

        // init repository

        // Asser.AreEqual(44, repository.AverageWeather())
        Assert.Pass();
    }
}
