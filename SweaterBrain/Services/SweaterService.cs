using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using SweaterBrain.Models;

namespace SweaterBrain.Services
{
  internal class SweaterService
  {
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _config;
    private readonly string _OPEN_WEATHER_KEY;

    public SweaterService(HttpClient httpClient, IConfiguration config)
    {
      _httpClient = httpClient;
      _config = config;
      _OPEN_WEATHER_KEY = _config["OPEN_WEATHER_KEY"];

    }

    public async Task<SuggesterDataDto> SuggesterData(string geoStr)
    {
      var _url = BuildQueryUrl(geoStr);
      var _response = await _httpClient.GetAsync(_url);
      var _body = await _response.Content.ReadAsStringAsync();
      var _openWeatherResponse = OpenWeatherResponse.FromJson(_body);

      return CompileSuggesterData(_openWeatherResponse);
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

    private string BuildQueryUrl(string geoStr)
    {
      string[] geoArr = geoStr.Split(',');
      string _lat = geoArr[0];
      string _lon = geoArr[1];
      return $"http://api.openweathermap.org/data/2.5/weather?lat={_lat}&lon={_lon}&appid={_OPEN_WEATHER_KEY}&units=imperial";
    }

    private string Path(string weight)
    {
      return $"https://futureengine.net/sweater/sweater_{weight}_2.jpg";
    }
  }
}
