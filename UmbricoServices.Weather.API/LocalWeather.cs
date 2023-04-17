using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace UmbricoServices.Weather.API;

public class LocalWeather
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string? City { get; set; }
    public double[]? Temperatures { get; set; }
    public DateTime? ReportedTime { get; set; }
}