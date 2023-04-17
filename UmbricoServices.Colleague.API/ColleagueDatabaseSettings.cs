namespace UmbricoServices.Colleague.API;

public class ColleagueDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string ColleagueCollectionName { get; set; } = null!;
}