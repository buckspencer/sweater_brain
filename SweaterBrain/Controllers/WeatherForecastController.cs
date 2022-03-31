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
    public Task<SuggesterDataDto> GetSuggeterData(string locationInfo)
    {
      IGeoService _geoService = new GeoService(_httpClient, _config);
      Task<SuggesterDataDto> returnedTemp = new SweaterService(_httpClient, _config, _geoService).SuggesterData(locationInfo);

      return returnedTemp;
    }
  }

}
