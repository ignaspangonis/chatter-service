namespace WeatherClient.Entities
{
    public interface IWeatherDto
    {
        public double Temperature { get; set; }
        public string Time { get; set; }
        public string Summary { get; set; }
    }

    public class WeatherDto
    {
        public WeatherDto()
        {
            Time = "";
            Summary = "";
        }

        public WeatherDto(double temperature, string time, string summary)
        {
            Temperature = temperature;
            Time = time;
            Summary = summary;
        }

        public double Temperature { get; set; }
        public string Time { get; set; }
        public string Summary { get; set; }
    }
}

