using Bogus;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace UmbricoServices.Weather.API;

public class WeatherService : IWeatherService
{
    private readonly IMongoCollection<LocalWeather> _weatherCollection;
    private const int SeedAmount = 100;

    public WeatherService(IOptions<WeatherDatabaseSettings> weatherDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            weatherDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            weatherDatabaseSettings.Value.DatabaseName);

        _weatherCollection = mongoDatabase.GetCollection<LocalWeather>(
            weatherDatabaseSettings.Value.WeatherCollectionName);
    }

    public async Task<List<LocalWeather>> GetAllWeatherReports() =>
        await _weatherCollection.Find(_ => true).ToListAsync();

    public async Task<LocalWeather?> GetWeatherForCity(string cityName) =>
        await _weatherCollection.Find(x => x.City == cityName).FirstOrDefaultAsync();

    public async Task PostWeatherReportForCity(WeatherRequest weatherRequest)
    {
        if (weatherRequest.city is null || weatherRequest.temperature is null) return;
        var weatherHistory = await _weatherCollection.Find(x => x.City == weatherRequest.city).FirstOrDefaultAsync();
        if (weatherHistory is null)
        {
            weatherHistory = new LocalWeather { City = weatherRequest.city, Temperatures = new[] { weatherRequest.temperature.Value }, ReportedTime = DateTime.Now};
            await _weatherCollection.InsertOneAsync(weatherHistory);
        }
        else
        {
            weatherHistory.Temperatures = weatherHistory.Temperatures.Concat(new [] {weatherRequest.temperature.Value} ).ToArray();
            weatherHistory.ReportedTime = DateTime.Now;
            await _weatherCollection.ReplaceOneAsync(x => x.Id == weatherHistory.Id, weatherHistory);
        }
    }

    public async Task PopulateDatabaseWithSeedData()
    {
        var currentData = await _weatherCollection.Find(_ => true).CountDocumentsAsync();
        if (currentData >= SeedAmount) return;
        
        Randomizer.Seed = new Random(1337);
        var seedReports = new Faker<LocalWeather>().StrictMode(true)
            .RuleFor(w => w.City, f => f.Address.City())
            .RuleFor(w => w.Temperatures, f => f.Make(10, () => f.Random.Double(-10d, 40d)).ToArray())
            .RuleFor(w => w.ReportedTime, f => f.Date.RecentOffset(-7).DateTime)
            .RuleFor(w => w.Id, _ => null);
        
        await _weatherCollection.InsertManyAsync(seedReports.Generate(SeedAmount));
    }
}