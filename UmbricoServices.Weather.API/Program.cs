using Bogus;
using UmbricoServices.Weather.API;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<WeatherDatabaseSettings>(
    builder.Configuration.GetSection("WeatherDatabase"));
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new (){ Title = "WeatherApi", Version = "v1"});
});
builder.Services.AddSingleton<WeatherService>();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Https disabled because no dev license
// app.UseHttpsRedirection();

app.MapGet("api/v1/weather", async(WeatherService weatherService)
    => await weatherService.GetAllWeatherReports());

app.MapGet("api/v1/weather/{city}", async(string cityName, WeatherService weatherService)
    => await weatherService.GetWeatherForCity(cityName));

app.MapPost("api/v1/weather", async(WeatherRequest weatherRequest, WeatherService weatherService)
    => await weatherService.PostWeatherReportForCity(weatherRequest));

await app.Services.GetRequiredService<WeatherService>().PopulateDatabaseWithSeedData();

app.Run();