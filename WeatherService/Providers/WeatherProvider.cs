using System;
using WeatherService.Entities;
using WeatherService.Transformers;
using Newtonsoft.Json;
using RestSharp;
using System.Text.Json;

namespace WeatherService.Providers
{
	public interface IWeatherProvider
	{
		public Task<WeatherDto> GetWeather();
    }

    public class WeatherProvider: IWeatherProvider
	{
        private readonly RestClient m_client;
		private readonly WeatherTransformers transformers;

        public WeatherProvider()
		{
			m_client = new RestClient("https://api.open-meteo.com");
            transformers = new WeatherTransformers();
        }

        public async Task<WeatherDto> GetWeather()
		{
			var request = new RestRequest($"/v1/forecast?latitude=54.69&longitude=25.28&current_weather=true", Method.Get);
			var response = await m_client.ExecuteAsync(request);
            var deserializedResponse = JsonConvert.DeserializeObject<WeatherResponse>(response.Content ?? "{}");
			var transformedResponse = transformers.TransformWeather(deserializedResponse);

			return transformedResponse;
		}
	}
}

