using SmartBike.Api.Data;

var builder = WebApplication.CreateBuilder(args);

// Controllers inschakelen
builder.Services.AddControllers();

// Repository registreren
builder.Services.AddScoped<ITelemetryRepository, TelemetryRepository>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();

// Controllers routen
app.MapControllers();

app.Run();
