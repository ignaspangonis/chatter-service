using System;
using WeatherClient.Providers;
using Moq;
using WeatherClient.Entities;
using WeatherClient.Transformers;
using NUnit.Framework;
using Newtonsoft.Json;
using WeatherClient.Utils;
using RestSharp;

namespace Tests;

public class Tests2
{
    [Test]
    public void TransformWeather_TransformsCorrectly()
    {
        var weatherDto = new WeatherDto(1.0, "2022-01-01T01:00", "cold");
        var weatherResponse = new WeatherResponse(new CurrentWeatherResponse(1.0, "2022-01-01T01:00"));
        var weatherTransformers = new WeatherTransformers();

        var transformedResult = weatherTransformers.TransformWeather(weatherResponse);

        Assert.That(JsonConvert.SerializeObject(weatherDto), Is.EqualTo(JsonConvert.SerializeObject(transformedResult)));
    }

    [Test]
    public void GetWeatherSummary_ReturnsCorrectSummary()
    {
        Assert.Multiple(() =>
        {
            Assert.That(WeatherUtils.GetWeatherSummary(-10), Is.EqualTo("very cold"));
            Assert.That(WeatherUtils.GetWeatherSummary(0), Is.EqualTo("cold"));
            Assert.That(WeatherUtils.GetWeatherSummary(10), Is.EqualTo("cool"));
            Assert.That(WeatherUtils.GetWeatherSummary(20), Is.EqualTo("warm"));
            Assert.That(WeatherUtils.GetWeatherSummary(30), Is.EqualTo("hot"));
        });
    }

    [Test]
    public void GetWeather_ReturnsCorrectWeather()
    {
        // Arrange
        string contentMock = "{\n    \"latitude\": 54.6875,\n    \"longitude\": 25.25,\n    \"generationtime_ms\": 0.2410411834716797,\n    \"utc_offset_seconds\": 0,\n    \"timezone\": \"GMT\",\n    \"timezone_abbreviation\": \"GMT\",\n    \"elevation\": 95.0,\n    \"current_weather\": {\n        \"temperature\": -7.2,\n        \"windspeed\": 8.0,\n        \"winddirection\": 234.0,\n        \"weathercode\": 3,\n        \"time\": \"2022-12-15T19:00\"\n    }\n}";
        var httpClientMock = new Mock<RestClient>();

        httpClientMock
            .Setup(
                m => m.ExecuteAsync(
                    It.IsAny<RestRequest>(),
                    It.IsAny<CancellationToken>())
            )
            .ReturnsAsync(new RestResponse() { Content = contentMock });

        var weatherProvider = new WeatherProvider(httpClientMock.Object);

        var weatherDto = new WeatherDto(-7.2, "2022-12-15T19:00", "very cold");

        // Act
        var response = weatherProvider.GetWeather();

        // Assert
        var expectedValue = JsonConvert.SerializeObject(response);
        var referenceValue = JsonConvert.SerializeObject(weatherDto);

        Assert.That(expectedValue, Is.EqualTo(referenceValue));

        // TODO assert response code
    }
}


