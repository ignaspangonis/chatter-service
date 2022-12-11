namespace WeatherClient.Entities
{
    public interface IWeatherDto
    {
        public double Temperature { get; set; }
        public string Time { get; set; }
    }

    public class WeatherDto
    {
        public double Temperature { get; set; }
        public string Time { get; set; } = string.Empty;
        public string Summary { get; set; } = string.Empty;
    }
}

