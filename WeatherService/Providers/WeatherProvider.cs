using System;
using WeatherService.Entities;
using WeatherService.Transformers;
using Newtonsoft.Json;
using RestSharp;
using System.Text.Json;

namespace WeatherService.Providers
{
    public class WeatherProvider
	{
        private readonly RestClient m_client;
		private readonly WeatherTransformers transformers;

        public WeatherProvider()
		{
			m_client = new RestClient("https://api.open-meteo.com");
            transformers = new WeatherTransformers();
        }

        public WeatherDto GetWeather()
		{
			var request = new RestRequest($"/v1/forecast?latitude=54.69&longitude=25.28&current_weather=true", Method.Get);
			var response = m_client.Execute(request);
            var deserializedResponse = JsonConvert.DeserializeObject<WeatherResponse>(response.Content ?? "{}");
			var transformedResponse = transformers.TransformWeather(deserializedResponse);

			return transformedResponse;
		}
	}
}

