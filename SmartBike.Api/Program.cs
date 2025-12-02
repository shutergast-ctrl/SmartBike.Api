using SmartBike.Api.Data;
var builder = WebApplication.CreateBuilder(args);
// ...
builder.Services.AddScoped<ITelemetryRepository, TelemetryRepository>();