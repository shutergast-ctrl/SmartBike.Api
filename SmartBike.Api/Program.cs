using SmartBike.Api.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Swagger registratie
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ITelemetryRepository, TelemetryRepository>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();

// Swagger UI alleen in Development
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
