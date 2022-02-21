using System.Threading.Tasks;

namespace SweaterBrain.Services
{
    internal interface ISweaterService
    {
        Task<object> WeatherInfoRequest();
    }
}