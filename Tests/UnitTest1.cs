namespace Tests;
using WeatherClient;
using WeatherClient.Interfaces;
using WeatherClient.Providers;

public class Tests
{
    IWeatherProvider provider;

    [OneTimeSetUp]
    public void Init()
    {
        var data = new List<WeatherClient.WeatherForecast> {
            new WeatherClient.WeatherForecast()
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
