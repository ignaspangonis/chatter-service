using System;
using WeatherClient.Providers;
using Moq;

namespace Tests;

public class Tests2
{
    IWeatherProvider provider;

    [OneTimeSetUp]
    public void Init()
    {
        var data = new List<WeatherClient.WeatherForecast> {
            new WeatherClient.WeatherForecast()
        };

        // init fake provider (mock provider)
        provider = new Mock<IWeatherProvider>(MockBehavior.Strict);
        // Loose - all good
        // Strict - kiekvienas iskvietimas turi buti sumockintas, kad butu predictable
        // turi zinoti ka tavo testas daro
        provider.Setup(m => m.GetSomeRepositoryShit(It.IsAny<String>, It.Is<String>)).Returns("");
    }

    [Test]
    public void Test1()
    {


        // init repositor
        var repository = new WeatherRepository(provider.Object);

        // Assert.AreEqual(44, repository.AverageWeather())
        Assert.Pass();
    }
}


