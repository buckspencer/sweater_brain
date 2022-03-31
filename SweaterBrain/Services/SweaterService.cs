using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using SweaterBrain.Models;

namespace SweaterBrain.Services
{
  internal class SweaterService : ISweaterService
  {
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _config;

    private readonly IGeoService _geoService;
    private readonly string _OPEN_WEATHER_KEY;

    public SweaterService(HttpClient httpClient, IConfiguration config, IGeoService geoService)
    {
      _geoService = geoService;
      _httpClient = httpClient;
      _config = config;
      _OPEN_WEATHER_KEY = _config["OPEN_WEATHER_KEY"];

    }

    public async Task<SuggesterDataDto> SuggesterData(string locationInfo)
    {
      MajorCityLocationDto locationResult = await _geoService.GeoBylocationInfoAsync(locationInfo);

      string _url = BuildQueryUrl(locationResult.LatLon);
      using (HttpResponseMessage _response = await _httpClient.GetAsync(_url))
      {
        string _body = await _response.Content.ReadAsStringAsync();
        OpenWeatherResponse _openWeatherResponse = OpenWeatherResponse.FromJson(_body);

        return CompileSuggesterData(_openWeatherResponse, locationResult);
      }
    }

    private SuggesterDataDto CompileSuggesterData(OpenWeatherResponse retrievedInfo, MajorCityLocationDto majorCityLocationDto)
    {
      double _temp = retrievedInfo.Main.Temp;
      string _feelsLike = retrievedInfo.Main.FeelsLike.ToString();
      var result = _temp switch
      {
        _ when _temp > 74 => new SuggesterDataDto(_temp.ToString(), _feelsLike, "Light", Path("light"), majorCityLocationDto),
        _ when _temp > 65 => new SuggesterDataDto(_temp.ToString(), _feelsLike, "Medium", Path("medium"), majorCityLocationDto),
        _ when _temp < 65 => new SuggesterDataDto(_temp.ToString(), _feelsLike, "Heavy", Path("heavy"), majorCityLocationDto),
        _ => throw new System.NotImplementedException()
      };

      return result;
    }

    private string BuildQueryUrl(List<string> LatLon)
    {
      string _lat = LatLon[0];
      string _lon = LatLon[1];
      return $"http://api.openweathermap.org/data/2.5/weather?lat={_lat}&lon={_lon}&appid={_OPEN_WEATHER_KEY}&units=imperial";
    }

    private string Path(string weight)
    {
      return $"https://futureengine.net/sweater/sweater_{weight}_2.jpg";
    }
  }
}
