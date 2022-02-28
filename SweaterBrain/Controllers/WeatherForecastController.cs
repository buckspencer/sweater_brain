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

        private List<MajorCityLocation> LocationList = new List<MajorCityLocation>()
        {
            new MajorCityLocation { CityName = "Los Angeles, Ca", Lat = "34", Lon = "-118" },
            new MajorCityLocation { CityName = "Athens, Ga.", Lat = "33", Lon = "83" },
            new MajorCityLocation { CityName = "Bagley", Lat = "47", Lon = "95" },
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(IConfiguration config, HttpClient httpClient, ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
            this._httpClient = httpClient;

            this._config = config;
        }

        [HttpGet("sweaterresult")]
        public Task<OpenWeatherResponse> GetSweater()
        {
            return new SweaterService(_httpClient, _config).RequestWeatherInfo();
        }


    }
}
