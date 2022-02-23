using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using SweaterBrain.Models;

namespace SweaterBrain.Services
{
    class SweaterService
    {
        // TODO: Currently lat&lon are hardcoded for Los Angeles, Ca
        private const string LAT = "34";
        private const string LON = "-118";

        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        private readonly string OPEN_WEATHER_API_URL;

        public SweaterService(HttpClient _httpClient, IConfiguration config)
        {
            this._httpClient = _httpClient;

            _config = config;
            var api_key = _config["SweaterBrain:OPEN_WEATHER:API_KEY"];
            OPEN_WEATHER_API_URL = $"http://api.openweathermap.org/data/2.5/weather?lat={LAT}&lon={LON}&appid={api_key}&units=imperial";
        }

        public async Task<OpenWeatherResponse> RequestWeatherInfo()
        {
            var response = await _httpClient.GetAsync(OPEN_WEATHER_API_URL);
            var body = await response.Content.ReadAsStringAsync();
            var openWeatherResponse = OpenWeatherResponse.FromJson(body);

            return openWeatherResponse;
        }

    }
}
