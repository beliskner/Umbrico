using UmbricoServices.Colleague.API;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ColleagueDatabaseSettings>(
    builder.Configuration.GetSection("ColleagueDatabase"));
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new (){ Title = "ColleagueApi", Version = "v1"});
});
builder.Services.AddSingleton<ColleagueService>();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Https disabled because no dev license
// app.UseHttpsRedirection();

app.MapGet("api/v1/colleagues", async(ColleagueService colleagueService)
    => await colleagueService.GetAllColleagues());

app.MapGet("api/v1/colleagues/{name}", async(string colleagueName, ColleagueService colleagueService)
    => await colleagueService.GetColleagueByName(colleagueName));

app.MapDelete("api/v1/colleagues/{id}", async(string colleagueId, ColleagueService colleagueService)
    => await colleagueService.DeleteColleagueById(colleagueId));

app.MapPost("api/v1/colleagues", async(ColleagueRequest colleagueRequest, ColleagueService colleagueService)
    => await colleagueService.AddColleague(colleagueRequest));

await app.Services.GetRequiredService<ColleagueService>().PopulateDatabaseWithSeedData();

app.Run();