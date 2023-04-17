using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UmbricoServices.Web.UI.Services;

namespace UmbricoServices.Web.UI.ViewComponents;

public class TemperatureChartViewComponent : ViewComponent
{
    private readonly IWeatherService _weatherService;

    public TemperatureChartViewComponent(IWeatherService weatherService)
    {
        _weatherService = weatherService;
    }

    public async Task<IViewComponentResult> InvokeAsync() => View((await _weatherService.GetAllWeatherReports()).OrderBy(x => Guid.NewGuid()).Take(5).ToArray());
}