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

        private readonly List<MajorCityLocation> LocationList = new List<MajorCityLocation>()
        {
            new MajorCityLocation { CityName = "Los Angeles, Ca", Lat = "34", Lon = "-118" },
            new MajorCityLocation { CityName = "Athens, Ga.", Lat = "33", Lon = "83" },
            new MajorCityLocation { CityName = "Bagley", Lat = "47", Lon = "95" },
        };

        public WeatherForecastController(IConfiguration config, ILogger<WeatherForecastController> logger)
        {
            this._config = config;
        }

        [HttpGet("suggester-data")]
        public Task<SuggesterDataDto> GetSuggeterData()
        {
            HttpClient _httpClient = new HttpClient();
            Task<SuggesterDataDto> returnedTemp = new SweaterService(_httpClient, _config).SuggesterData();

            return returnedTemp;
        }


    }
}
