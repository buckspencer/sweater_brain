using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SweaterBrain.Services
{

    class SweaterService : ISweaterService
    {
        // TODO: Key to secure storage.

        // TODO: Currently lat&lon are hardcoded for Los Angeles, Ca
        public const string LAT = "34";
        public const string LON = "-118";
        public string OPEN_WEATHER_API_URL = $"http://api.openweathermap.org/data/2.5/weather?lat={LAT}&lon={LON}&appid={API_KEY}&units=imperial";



        public async Task<object> WeatherInfoRequest()
        {
            using var client = new HttpClient();
            string result = await client.GetStringAsync(OPEN_WEATHER_API_URL);

            object jsonString = JsonConvert.DeserializeObject(result);
            return jsonString;
        }
    }
}
