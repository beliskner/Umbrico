namespace UmbricoServices.Colleague.API;

public interface IColleagueService
{
    Task<List<Colleague>> GetAllColleagues();
    Task<Colleague?> GetColleagueByName(string name);
    Task DeleteColleagueById(string id);
    Task AddColleague(ColleagueRequest colleagueRequest);
    Task PopulateDatabaseWithSeedData();
}