using System;
using Newtonsoft.Json;

namespace WeatherService.Entities
{
    public class WeatherDto
    {
        public double Temperature { get; set; }
        public string Time { get; set; } = string.Empty;
    }
}

