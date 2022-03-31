using System.Threading.Tasks;
using SweaterBrain.Models;

namespace SweaterBrain.Services
{
  public interface IGeoService
  {
    public Task<MajorCityLocationDto> GeoBylocationInfoAsync(string locationInfo);
  }
}
