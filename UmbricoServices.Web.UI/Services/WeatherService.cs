using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using UmbricoServices.Web.UI.Infrastructure;
using UmbricoServices.Web.UI.ViewModels;

namespace UmbricoServices.Web.UI.Services;

public class WeatherService : IWeatherService
{
    private readonly HttpClient _httpClient;
    private readonly string _requestBaseUri;

    public WeatherService(HttpClient httpClient, IOptionsMonitor<WeatherServiceOptions> optionsMonitor)
    {
        _httpClient = httpClient;
        _requestBaseUri = $"{optionsMonitor.CurrentValue.BaseUri}/api/v{optionsMonitor.CurrentValue.ApiVersion}";
    }

    public async Task<LocalWeatherViewModel[]> GetAllWeatherReports()
    {
        var uri = API.Weather.GetAllLocalWeatherReports(_requestBaseUri);

        var responseString = await _httpClient.GetStringAsync(uri);

        return JsonSerializer.Deserialize<LocalWeatherViewModel[]>(responseString, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        }) ?? Array.Empty<LocalWeatherViewModel>();
    }

    public async Task<LocalWeatherViewModel?> GetWeatherForCity(string cityName)
    {
        var uri = API.Colleague.GetColleagueByName(_requestBaseUri, cityName);

        var responseString = await _httpClient.GetStringAsync(uri);

        var weatherReport = JsonSerializer.Deserialize<LocalWeatherViewModel>(responseString, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return weatherReport;
    }

    public async Task<LocalWeatherViewModel?> PostWeatherReportForCity(LocalWeatherViewModel localWeatherViewModel)
    {
        var uri = API.Weather.PostWeatherForCity(_requestBaseUri);

        var weatherContent = new StringContent(JsonSerializer.Serialize(localWeatherViewModel), System.Text.Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(uri, weatherContent);

        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();
        
        return JsonSerializer.Deserialize<LocalWeatherViewModel>(responseString, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
    }
}