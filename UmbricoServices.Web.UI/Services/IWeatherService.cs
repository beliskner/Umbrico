using System.Threading.Tasks;
using UmbricoServices.Web.UI.ViewModels;

namespace UmbricoServices.Web.UI.Services;

public interface IWeatherService
{
    Task<LocalWeatherViewModel[]> GetAllWeatherReports();
    Task<LocalWeatherViewModel?> GetWeatherForCity(string cityName);
    Task<LocalWeatherViewModel?> PostWeatherReportForCity(LocalWeatherViewModel localWeatherViewModel);
}