using WeatherApp.Constants;
namespace WeatherApp.Helper
{
	public static class Utils
	{
		
		public static string BuildCityWeatherUrl(string lat,string longi)
		{
            var url = $"{Constant.weatherApiUrl}?appid={Constant.apiKey}&lat={lat}&lon={longi}";
			return url;
		}
	}
}

