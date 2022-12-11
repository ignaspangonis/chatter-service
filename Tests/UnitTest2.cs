using System;
using WeatherClient.Providers;
using Moq;
using WeatherClient.Entities;
using WeatherClient.Transformers;

namespace Tests;

public class Tests2
{

    [OneTimeSetUp]
    public void Init()
    {
        //var data = new List<WeatherClient.WeatherForecast> {
        //    new WeatherClient.WeatherForecast()
        //};

        //// init fake provider (mock provider)
        //provider = new Mock<IWeatherProvider>(MockBehavior.Strict);
        //// Loose - all good
        //// Strict - kiekvienas iskvietimas turi buti sumockintas, kad butu predictable
        //// turi zinoti ka tavo testas daro
        //provider.Setup(m => m.GetSomeRepositoryShit(It.IsAny<String>, It.Is<String>)).Returns("");
    }

    [Test]
    public void Test1()
    {
        var weatherDto = new WeatherDto(1.0, "2022-01-01T01:00", "cold");
        var weatherResponse = new WeatherResponse(new CurrentWeatherResponse(1.0, "2022-01-01T01:00"));
        var weatherTransformers = new WeatherTransformers();

        Assert.That(weatherDto, Is.EqualTo(weatherTransformers.TransformWeather(weatherResponse)));
    }
}


