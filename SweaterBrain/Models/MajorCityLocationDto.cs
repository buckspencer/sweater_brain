using System.Collections.Generic;

namespace SweaterBrain.Models
{
  public class MajorCityLocationDto
  {
    public MajorCityLocationDto(string cityName, List<string> latLon)
    {
      CityName = cityName;
      LatLon = latLon;
    }

    public string CityName { get; }
    public List<string> LatLon { get; }
  }

}
