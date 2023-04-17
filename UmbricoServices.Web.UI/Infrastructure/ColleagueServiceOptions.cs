namespace UmbricoServices.Web.UI.Infrastructure;

public class ColleagueServiceOptions
{
    public const string ColleagueService = "ColleagueService";
    public string BaseUri { get; set; } = string.Empty;
    public int ApiVersion { get; set; } = 1;
}