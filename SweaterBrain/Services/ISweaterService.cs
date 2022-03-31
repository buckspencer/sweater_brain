using System.Threading.Tasks;
using SweaterBrain.Models;

namespace SweaterBrain.Services
{
  internal interface ISweaterService
  {
    Task<SuggesterDataDto> SuggesterData(string locationInfo);
  }
}
