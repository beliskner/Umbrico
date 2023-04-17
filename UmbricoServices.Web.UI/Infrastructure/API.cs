namespace UmbricoServices.Web.UI.Infrastructure;

public static class API
{
    public static class Colleague
    {
        public static string GetAllColleagues(string baseUri) => ColleagueHub(baseUri);
        public static string CreateColleague(string baseUri) => ColleagueHub(baseUri);
        public static string GetColleagueByName(string baseUri, string name) => $"{baseUri}/colleagues/{name}";
        public static string DeleteColleagueById(string baseUri, string id) => $"{baseUri}/colleagues/{id}";

        private static string ColleagueHub(string baseUri) => $"{baseUri}/colleagues";
    }

    public static class Weather
    {
        public static string GetAllLocalWeatherReports(string baseUri) => $"{baseUri}/weather";
        public static string GetWeatherForCity(string baseUri, string cityName) => $"{baseUri}/weather/{cityName}";
        public static string PostWeatherForCity(string baseUri) => $"{baseUri}/weather";
    }
}