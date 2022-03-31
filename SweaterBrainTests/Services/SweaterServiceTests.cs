using Microsoft.Extensions.Configuration;
using Moq;
using Moq.Protected;
using SweaterBrain.Models;
using SweaterBrain.Services;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace SweaterBrainTests
{
  public class SweaterServiceTests
  {
    [Fact]
    public async Task ReturnsSuggesterDtoWithProperlySetAttributes()
    {

      //Mock Client
      Mock<HttpMessageHandler> _handlerMockWeather = new Mock<HttpMessageHandler>();
      HttpClient _httpClientWeather = new HttpClient(_handlerMockWeather.Object);
      _ = _handlerMockWeather.Protected()
      .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
      .ReturnsAsync(new HttpResponseMessage
      {
        StatusCode = HttpStatusCode.OK,
        Content = new StringContent(@"{""coord"":{""lon"":-118,""lat"":34},""weather"":[{""id"":802,""main"":""Clouds"",""description"":""scattered clouds"",""icon"":""03d""}],""base"":""stations"",""main"":{""temp"":49.98,""feels_like"":48.43,""temp_min"":46.42,""temp_max"":53.46,""pressure"":1015,""humidity"":78},""visibility"":10000,""wind"":{""speed"":4.61,""deg"":190},""clouds"":{""all"":40},""dt"":1645545947,""sys"":{""type"":2,""id"":2009710,""country"":""US"",""sunrise"":1645540148,""sunset"":1645580529},""timezone"":-28800,""id"":5354819,""name"":""Hacienda Heights"",""cod"":200}"),
      });

      //Mock config
      Mock<IConfiguration> _configuration = new Mock<IConfiguration>();
      Mock<IConfigurationSection> _configSection = new Mock<IConfigurationSection>();
      _ = _configuration.Setup(x => x.GetSection("MySection:Value")).Returns(_configSection.Object);


      //Mock GeoService
      Mock<IGeoService> _geoService = new Mock<IGeoService>();
      _geoService
      .Setup(x => x.GeoBylocationInfoAsync(It.IsAny<string>()))
      .ReturnsAsync( new MajorCityLocationDto("Los Angeles, Ca", new List<string> { "34", "-118" }) );


      //Call service
      SweaterService _sweaterService = new SweaterService(_httpClientWeather, _configSection.Object, _geoService.Object);

      string locationInfo = "90210";
      SuggesterDataDto _result = await _sweaterService.SuggesterData(locationInfo);

      // Assert returned types as expected
      Assert.IsType<SuggesterDataDto>(_result);
      Assert.IsType<string>(_result.Temp);
      Assert.IsType<string>(_result.FeelsLike);
      Assert.IsType<string>(_result.Weight);
      Assert.IsType<string>(_result.SweaterPath);
      Assert.IsType<MajorCityLocationDto>(_result.MajorCityLocationDto);

      _handlerMockWeather.Protected().Verify(
         "SendAsync",
         Times.Exactly(1),
         ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
         ItExpr.IsAny<CancellationToken>());

    }
  }
}

