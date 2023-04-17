namespace UmbricoServices.Weather.API;

public record WeatherRequest(string? city, double? temperature);