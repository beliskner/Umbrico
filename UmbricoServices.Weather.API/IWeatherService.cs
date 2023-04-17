namespace UmbricoServices.Weather.API;

public interface IWeatherService
{
    Task<List<LocalWeather>> GetAllWeatherReports();
    Task<LocalWeather?> GetWeatherForCity(string cityName);
    Task PostWeatherReportForCity(WeatherRequest weatherRequest);
    Task PopulateDatabaseWithSeedData();
}