using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SweaterBrain.Services;

namespace SweaterBrain.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private List<MajorCityLocation> LocationList = new List<MajorCityLocation>()
        {
            new MajorCityLocation { CityName = "Los Angeles, Ca", Lat = "34", Lon = "-118" },
            new MajorCityLocation { CityName = "Athens, Ga.", Lat = "33", Lon = "83" },
            new MajorCityLocation { CityName = "Bagley", Lat = "47", Lon = "95" },
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        //[HttpGet("sweaterresult")]
        //public object GetSweater()
        //{
        //    Task<Models.OpenWeatherResponse> task = SweaterService.RequestWeatherInfo();
        //}


    }
}
