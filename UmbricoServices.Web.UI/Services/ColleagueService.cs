using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using UmbricoServices.Web.UI.Infrastructure;
using UmbricoServices.Web.UI.ViewModels;

namespace UmbricoServices.Web.UI.Services;

public class ColleagueService : IColleagueService
{
    private readonly HttpClient _httpClient;
    private readonly string _requestBaseUri;

    public ColleagueService(IOptionsMonitor<ColleagueServiceOptions> optionsMonitor, HttpClient httpClient)
    {
        _httpClient = httpClient;
        _requestBaseUri = $"{optionsMonitor.CurrentValue.BaseUri}/api/v{optionsMonitor.CurrentValue.ApiVersion}";
    }

    public async Task<ColleagueViewModel[]> GetAllColleagues()
    {
        var uri = API.Colleague.GetAllColleagues(_requestBaseUri);

        var responseString = await _httpClient.GetStringAsync(uri);

        return JsonSerializer.Deserialize<ColleagueViewModel[]>(responseString, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        }) ?? Array.Empty<ColleagueViewModel>();
    }

    public async Task<ColleagueViewModel?> GetColleagueByName(string name)
    {
        var uri = API.Colleague.GetColleagueByName(_requestBaseUri, name);

        var responseString = await _httpClient.GetStringAsync(uri);

        return JsonSerializer.Deserialize<ColleagueViewModel>(responseString, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
    }

    public async Task<bool> DeleteColleagueById(string id)
    {
        var uri = API.Colleague.DeleteColleagueById(_requestBaseUri, id);
        
        var response = await _httpClient.DeleteAsync(uri);

        return response.IsSuccessStatusCode;
    }

    public async Task<ColleagueViewModel?> AddColleague(ColleagueViewModel colleagueViewModel)
    {
        var uri = API.Colleague.CreateColleague(_requestBaseUri);

        var colleagueContent = new StringContent(JsonSerializer.Serialize(colleagueViewModel), System.Text.Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(uri, colleagueContent);

        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();
        
        return JsonSerializer.Deserialize<ColleagueViewModel>(responseString, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
    }
}