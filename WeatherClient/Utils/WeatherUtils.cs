namespace WeatherClient.Utils
{
	public static class WeatherUtils
	{
		public static string GetWeatherSummary(double? temperature)
		{
            if (temperature == null) return "unknown";

			if (24 < temperature) return "hot";
            if (19 < temperature && temperature <= 24) return "warm";
            if (14 < temperature && temperature <= 19) return "moderate";
            if (5 < temperature && temperature <= 14) return "cool";
            if (-2 < temperature && temperature <= 5) return "cold";

            return "very cold";
        }
    }
}

