using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace UmbricoServices.Colleague.API;

public class Colleague
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Department { get; set; }
    public int? AvailableMinutesToday { get; set; }
    public bool? InTheMoodForPotjePingPong { get; set; }
    public string? FavoriteCatchPhrase { get; set; }
    public string? ImageUrl { get; set; }
}