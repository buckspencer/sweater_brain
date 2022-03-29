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
        private readonly IConfiguration _config;

        public WeatherForecastController(IConfiguration config, ILogger<WeatherForecastController> _logger)
        {
            this._config = config;
        }

        [HttpGet("suggester-data")]
        public Task<SuggesterDataDto> GetSuggeterData(string cityGeo)
        {

            HttpClient _httpClient = new HttpClient();

            Task<SuggesterDataDto> returnedTemp = new SweaterService(_httpClient, _config).SuggesterData(cityGeo);

            return returnedTemp;
        }

        [HttpGet("city-selection")]
        public List<MajorCityLocationDto> GetCitySelection()
        {
            var _locations = new List<MajorCityLocationDto>();
            _locations.Add(new MajorCityLocationDto("Los Angeles, Ca", new List<string> { "34", "-118" } ));
            _locations.Add(new MajorCityLocationDto("Athens, Ga.", new List<string> { "33", "-83" } ));
            _locations.Add(new MajorCityLocationDto("Bagley, MN", new List<string> { "47", "-95" } ));
            return _locations;
        }
    }

}
