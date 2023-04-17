using Microsoft.Extensions.DependencyInjection;
using Umbraco.Cms.Core.Composing;
using UmbricoServices.Web.UI.Services;

namespace UmbricoServices.Web.UI.Infrastructure;

public class ServiceComposer : IComposer
{
    public void Compose(IUmbracoBuilder builder)
    {
        builder.Services.Configure<ColleagueServiceOptions>(
            builder.Config.GetSection(ColleagueServiceOptions.ColleagueService));
        builder.Services.Configure<WeatherServiceOptions>(
            builder.Config.GetSection(WeatherServiceOptions.WeatherService));

        builder.Services.AddTransient<IColleagueService, ColleagueService>();
        builder.Services.AddTransient<IWeatherService, WeatherService>();

        builder.Services.AddHttpClient<IColleagueService, ColleagueService>();
        builder.Services.AddHttpClient<IWeatherService, WeatherService>();
    }
}