using System;
using System.Threading.Tasks;
using WeatherApp.Helper;
using Newtonsoft.Json;

namespace WeatherApp.Service
{
    public class WeatherService
    {
       public static RestHelper client;

        public WeatherService()
        {
            client = new RestHelper();
        }

        public async Task<Root> getWeatherByCity(string lat,string longi)
        {
            var url = Utils.BuildCityWeatherUrl(lat,longi);
            Console.WriteLine(url);
            var content = await client.Get(url);
            var response = JsonConvert.DeserializeObject<Root>(content);
          
            return response;
        }

    }
}

