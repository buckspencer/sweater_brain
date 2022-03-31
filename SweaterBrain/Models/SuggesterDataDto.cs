
namespace SweaterBrain.Models
{
  public class SuggesterDataDto
  {
    public SuggesterDataDto(string temp, string feelsLike, string weight, string sweaterPath, MajorCityLocationDto _majorCityLocationDto)
    {
      Temp = temp;
      FeelsLike = feelsLike;
      Weight = weight;
      SweaterPath = sweaterPath;
      MajorCityLocationDto = _majorCityLocationDto;
    }

    public string Temp { get; }
    public string FeelsLike { get; }
    public string Weight { get; }
    public string SweaterPath { get; }
    public MajorCityLocationDto MajorCityLocationDto { get; }
  }
}
