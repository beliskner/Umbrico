using Bogus;
using Bogus.DataSets;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace UmbricoServices.Colleague.API;

public class ColleagueService : IColleagueService
{
    private readonly IMongoCollection<Colleague> _colleagueCollection;
    private const int SeedAmount = 20;

    public ColleagueService(IOptions<ColleagueDatabaseSettings> colleagueDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            colleagueDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            colleagueDatabaseSettings.Value.DatabaseName);

        _colleagueCollection = mongoDatabase.GetCollection<Colleague>(
            colleagueDatabaseSettings.Value.ColleagueCollectionName);
    }

    public async Task<List<Colleague>> GetAllColleagues() =>
        await _colleagueCollection.Find(_ => true).ToListAsync();

    public async Task<Colleague?> GetColleagueByName(string name) =>
        await _colleagueCollection.Find(c => c.Name == name).FirstOrDefaultAsync();

    public async Task DeleteColleagueById(string id) =>
        await _colleagueCollection.FindOneAndDeleteAsync(c => c.Id == id);

    public async Task AddColleague(ColleagueRequest colleagueRequest)
    {
        if (colleagueRequest.name is null) return;
        var colleague = await _colleagueCollection.Find(x => x.Name == colleagueRequest.name).FirstOrDefaultAsync();
        if (colleague is not null) return;

        colleague = new Colleague
        {
            Name = colleagueRequest.name,
            Department = colleagueRequest.department,
            AvailableMinutesToday = colleagueRequest.availableMinutesToday,
            InTheMoodForPotjePingPong = colleagueRequest.inTheMoodForPotjePingpong ?? false,
            FavoriteCatchPhrase = colleagueRequest.favoriteCatchPhrase,
            ImageUrl = colleagueRequest.imageUrl
        };
        
        await _colleagueCollection.InsertOneAsync(colleague);
    }

    public async Task PopulateDatabaseWithSeedData()
    {
        var currentData = await _colleagueCollection.Find(_ => true).CountDocumentsAsync();
        if (currentData >= SeedAmount) return;
        
        Randomizer.Seed = new Random(1337);
        var seedReports = new Faker<Colleague>().StrictMode(true)
            .RuleFor(c => c.Name, f => f.Name.FullName())
            .RuleFor(c => c.Department, f => f.Commerce.Department())
            .RuleFor(c => c.AvailableMinutesToday, f => f.Random.Int(0, 8*60))
            .RuleFor(c => c.InTheMoodForPotjePingPong, f => f.Random.Bool(0.1f))
            .RuleFor(c => c.FavoriteCatchPhrase, f => f.Company.CatchPhrase())
            .RuleFor(c => c.ImageUrl, f => f.Internet.Avatar())
            .RuleFor(w => w.Id, _ => null);

        var seedData = seedReports.Generate(SeedAmount);
        
        await _colleagueCollection.InsertManyAsync(seedData);
    }
}