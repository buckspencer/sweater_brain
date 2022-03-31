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
  public class GeoServiceTests
  {
    [Fact]
    public async Task ReturnsMajorCityDtoWithProperlySetAttributes()
    {
      Mock<IConfiguration> configuration = new Mock<IConfiguration>();
      Mock<IConfigurationSection> configSection = new Mock<IConfigurationSection>();
      Mock<HttpMessageHandler> _handlerMockGeo = new Mock<HttpMessageHandler>();
      _ = _handlerMockGeo.Protected()
      .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
      .ReturnsAsync(new HttpResponseMessage
      {
        StatusCode = HttpStatusCode.OK,
        Content = new StringContent(@"{ ""results"": [ { ""address_components"": [ { ""long_name"": ""90210"", ""short_name"": ""90210"", ""types"": [ ""postal_code"" ] }, { ""long_name"": ""Beverly Hills"", ""short_name"": ""Beverly Hills"", ""types"": [ ""locality"", ""political"" ] }, { ""long_name"": ""Los Angeles County"", ""short_name"": ""Los Angeles County"", ""types"": [ ""administrative_area_level_2"", ""political"" ] }, { ""long_name"": ""California"", ""short_name"": ""CA"", ""types"": [ ""administrative_area_level_1"", ""political"" ] }, { ""long_name"": ""United States"", ""short_name"": ""US"", ""types"": [ ""country"", ""political"" ] } ], ""formatted_address"": ""Beverly Hills, CA 90210, USA"", ""geometry"": { ""bounds"": { ""northeast"": { ""lat"": 34.1354771, ""lng"": -118.3867129 }, ""southwest"": { ""lat"": 34.065094, ""lng"": -118.4423781 } }, ""location"": { ""lat"": 34.1030032, ""lng"": -118.4104684 }, ""location_type"": ""APPROXIMATE"", ""viewport"": { ""northeast"": { ""lat"": 34.1354771, ""lng"": -118.3867129 }, ""southwest"": { ""lat"": 34.065094, ""lng"": -118.4423781 } } }, ""place_id"": ""ChIJ7xfS-zW8woARXNkAJzX5Hs8"", ""postcode_localities"": [ ""Beverly Glen"", ""Beverly Hills"", ""Central LA"", ""Hollywood Hills"", ""Los Angeles"", ""Sherman Oaks"", ""Studio City"", ""The Flats"" ], ""types"": [ ""postal_code"" ] } ], ""status"": ""OK"" }"),
      });

      HttpClient httpClient = new HttpClient(_handlerMockGeo.Object);
      _ = configuration.Setup(x => x.GetSection("MySection:Value")).Returns(configSection.Object);
      GeoService _geoService = new GeoService(httpClient, configSection.Object);

      var locationInfo = "90210";
      MajorCityLocationDto retrievedInfo = await _geoService.GeoBylocationInfoAsync(locationInfo);

      Assert.IsType<MajorCityLocationDto>(retrievedInfo);

      var testCityName = "Beverly Hills, CA 90210, USA";
      var testLatLon = new List<string> { "34.1030032", "-118.4104684" };
      Assert.Equal(testCityName, retrievedInfo.CityName);
      Assert.Equal(testLatLon, retrievedInfo.LatLon);

      _handlerMockGeo.Protected().Verify(
      "SendAsync",
      Times.Exactly(1),
      ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
      ItExpr.IsAny<CancellationToken>());

    }
  }
}

