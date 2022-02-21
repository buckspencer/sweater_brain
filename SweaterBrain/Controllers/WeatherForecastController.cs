using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace SweaterBrain.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private List<MajorCityLocation> LocationList = new List<MajorCityLocation>()
        {
            new MajorCityLocation { CityName = "Los Angeles, Ca", Lat = "34°04'N", Lon = "s118°15'W" },
            new MajorCityLocation { CityName = "Athens, Ga.", Lat = "33°57'N", Lon = "83°23'W" },
            new MajorCityLocation { CityName = "Bagley", Lat = "47°32'N", Lon = "95°24'W" },
        };



        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet("locationcords")]
        public IEnumerable<MajorCityLocation> GetCities()
        {
            return LocationList;
        }

        [HttpGet("list")]
        public IEnumerable<WeatherForecast> GetWeather()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("sweaterresult")]
        public async Task<object> GetSweaterAsync()
        {
            var lat = "34";
            var lon = "-118";
            var url = $"http://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&appid={apiKey}&units=imperial";
            using var client = new HttpClient();
            string result = await client.GetStringAsync(url);

            object jsonString = JsonConvert.DeserializeObject(result);
            return jsonString;
        }


    }
}
