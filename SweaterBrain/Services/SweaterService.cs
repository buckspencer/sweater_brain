using System.Net.Http;
using System.Threading.Tasks;
using SweaterBrain.Models;

namespace SweaterBrain.Services
{
    class SweaterService
    {
        // TODO: Key to secure storage.
        public string OPEN_WEATHER_API_URL = $"http://api.openweathermap.org/data/2.5/weather?lat={LAT}&lon={LON}&appid={OPEN_WEATHER_API_KEY}&units=imperial";

        // TODO: Currently lat&lon are hardcoded for Los Angeles, Ca
        public const string LAT = "34";
        public const string LON = "-118";

        private readonly HttpClient _httpClient;

        public SweaterService(HttpClient _httpClient)
        {
            this._httpClient = _httpClient;
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
