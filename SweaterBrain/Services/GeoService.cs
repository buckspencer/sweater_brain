using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using SweaterBrain.Models;

namespace SweaterBrain.Services
{
  internal class GeoService : IGeoService
  {
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _config;
    private readonly string _GOOGLE_KEY;

    public GeoService(HttpClient httpClient, IConfiguration config)
    {
      _httpClient = httpClient;
      _config = config;
      _GOOGLE_KEY = _config["GOOGLE_KEY"];
    }

    public async Task<MajorCityLocationDto> GeoBylocationInfoAsync(string locationInfo)
    {
      var requestUri = RequestUrl(locationInfo);

      using (HttpResponseMessage _response = await _httpClient.GetAsync(requestUri))
      {
        string _body = await _response.Content.ReadAsStringAsync();

        GeoCodeResponse _geoCodeResponse = GeoCodeResponse.FromJson(_body);
        MajorCityLocationDto _mappedDto = MajorCityLocationDtoFromResponse(_geoCodeResponse);

        return _mappedDto;
      }
    }

    private MajorCityLocationDto MajorCityLocationDtoFromResponse(GeoCodeResponse geoCodeResponse)
    {
      string _resultAddr = geoCodeResponse.Results[0].FormattedAddress;
      double _lat = geoCodeResponse.Results[0].Geometry.Location.Lat;
      double _lon = geoCodeResponse.Results[0].Geometry.Location.Lng;
      MajorCityLocationDto _mapToDto = new MajorCityLocationDto(
          _resultAddr,
          new List<string> { _lat.ToString(), _lon.ToString() }
      );

      return _mapToDto;
    }

    private string RequestUrl(string postalCode)
    {
      return $"https://maps.googleapis.com/maps/api/geocode/json?sensor=true&address={postalCode}&key={_GOOGLE_KEY}&units=imperial";
    }
  }
}
