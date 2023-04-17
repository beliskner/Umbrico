namespace UmbricoServices.Colleague.API;

public record ColleagueRequest(string? name, string? department, int? availableMinutesToday, bool? inTheMoodForPotjePingpong, string? favoriteCatchPhrase, string? imageUrl);