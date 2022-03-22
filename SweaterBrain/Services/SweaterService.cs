using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SweaterBrain.Models;

namespace SweaterBrain.Services
{
    internal class SweaterService
    {
        // TODO: Currently lat&lon are hardcoded for Los Angeles, Ca, plans are to augment for more cities.
        private const string LAT = "34";
        private const string LON = "-118";

        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        private readonly string OPEN_WEATHER_API_URL;

        public SweaterService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
            var api_key = _config["open_weather"];
            OPEN_WEATHER_API_URL = $"http://api.openweathermap.org/data/2.5/weather?lat={LAT}&lon={LON}&appid={api_key}&units=imperial";
        }

        public async Task<SuggesterDataDto> SuggesterData()
        {
            var response = await _httpClient.GetAsync(OPEN_WEATHER_API_URL);
            var body = await response.Content.ReadAsStringAsync();
            var openWeatherResponse = OpenWeatherResponse.FromJson(body);

            return CompileSuggesterData(openWeatherResponse);
        }

        private SuggesterDataDto CompileSuggesterData(OpenWeatherResponse retrievedInfo)
        {
            double _temp = retrievedInfo.Main.Temp;
            string _feelsLike = retrievedInfo.Main.FeelsLike.ToString();
            var result = _temp switch
            {
                _ when _temp > 74 => new SuggesterDataDto(_temp.ToString(), _feelsLike, "Light", Path("light")),
                _ when _temp > 65 => new SuggesterDataDto(_temp.ToString(), _feelsLike, "Medium", Path("medium")),
                _ when _temp < 65 => new SuggesterDataDto(_temp.ToString(), _feelsLike, "Heavy", Path("heavy")),
                _ => throw new System.NotImplementedException()
            };

            return result;
        }

        private string Path(string weight)
        {
            return $"https://futureengine.net/sweater/sweater_{weight}_2.jpg";
        }
    }
}
