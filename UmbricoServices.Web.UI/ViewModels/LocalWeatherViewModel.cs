using System;

namespace UmbricoServices.Web.UI.ViewModels;

public class LocalWeatherViewModel
{
    public string? City { get; set; }
    public double[]? Temperatures { get; set; }
    public DateTime? ReportedTime { get; set; }
}