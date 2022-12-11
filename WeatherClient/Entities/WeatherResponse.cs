using Newtonsoft.Json;

namespace WeatherClient.Entities
{
    public interface ICurrentWeatherResponse
    {
        public double Temperature { get; set; }
        public double Windspeed { get; set; }
        public double Winddirection { get; set; }
        public int Weathercode { get; set; }
        public string? Time { get; set; }
    }

    public class CurrentWeatherResponse: ICurrentWeatherResponse
    {
        public double Temperature { get; set; }
        public double Windspeed { get; set; }
        public double Winddirection { get; set; }
        public int Weathercode { get; set; }
        public string? Time { get; set; }
    }

    public interface IWeatherResponse
    {
        public double Latitude { get; set; }

        public double Longitude { get; set; }

        [JsonProperty("generationtime_ms")]
        public double GenerationtimeMs { get; set; }

        [JsonProperty("utc_offset_seconds")]
        public int UtcOffsetSeconds { get; set; }

        public string? Timezone { get; set; }

        [JsonProperty("timezone_abbreviation")]
        public string? TimezoneAbbreviation { get; set; }

        public double Elevation { get; set; }

        [JsonProperty("current_weather")]
        public CurrentWeatherResponse CurrentWeather { get; set; }
    }

    public class WeatherResponse : IWeatherResponse
    {
        public double Latitude { get; set; }

        public double Longitude { get; set; }

        [JsonProperty("generationtime_ms")]
        public double GenerationtimeMs { get; set; }

        [JsonProperty("utc_offset_seconds")]
        public int UtcOffsetSeconds { get; set; }

        public string? Timezone { get; set; }

        [JsonProperty("timezone_abbreviation")]
        public string? TimezoneAbbreviation { get; set; }

        public double Elevation { get; set; }

        [JsonProperty("current_weather")]
        public CurrentWeatherResponse? CurrentWeather { get; set; }
    }
}

