namespace UmbricoServices.Web.UI.Infrastructure;

public class WeatherServiceOptions
{
    public const string WeatherService = "WeatherService";
    public string BaseUri { get; set; } = string.Empty;
    public int ApiVersion { get; set; } = 1;
}