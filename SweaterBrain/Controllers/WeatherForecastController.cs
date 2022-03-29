using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SweaterBrain.Models;
using SweaterBrain.Services;

namespace SweaterBrain.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class WeatherForecastController : ControllerBase
  {
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _config;

    public WeatherForecastController(IConfiguration config, ILogger<WeatherForecastController> _logger)
    {
      this._config = config;
      this._httpClient = new HttpClient();
    }

    [HttpGet("suggester-data")]
    public Task<SuggesterDataDto> GetSuggeterData(string cityGeo)
    {

      Task<SuggesterDataDto> returnedTemp = new SweaterService(_httpClient, _config).SuggesterData(cityGeo);

      return returnedTemp;
    }

    [HttpGet("city-selection")]
    public List<MajorCityLocationDto> GetCitySelection()
    {
        List<MajorCityLocationDto> _locations = new List<MajorCityLocationDto>
        {
            new MajorCityLocationDto("Los Angeles, Ca", new List<string> { "34", "-118" }),
            new MajorCityLocationDto("Athens, Ga.", new List<string> { "33", "-83" }),
            new MajorCityLocationDto("Bagley, MN", new List<string> { "47", "-95" })
        };
        return _locations;
    }

    [HttpGet("city-by-zip")]
    public Task<MajorCityLocationDto> GetCityByZip(string postalCode)
    {
        Task<MajorCityLocationDto> result = new GeoService(_httpClient, _config).GeoByPostalCodeAsync(postalCode);

        return result;
    }
  }

}
