using System.Reflection;
using System.Text.Json.Serialization;
using Fleet.Files.Repository;
using Fleet.Files.Services;
using Fleet.Vehicles.Extensions;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var services = builder.Services;

// Add services to the container.
services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyMethod(); ;
        builder.WithOrigins("http://localhost:4200", "https://localhost:4200");
        builder.AllowAnyHeader();
        builder.AllowCredentials();
    });
});
services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

services.AddVehicleService(configuration.GetSection("VehicleService"), Assembly.GetExecutingAssembly());
services.AddFileServices();
services.AddFilesRepository(configuration.GetConnectionString("FilesDbContext"), Assembly.GetExecutingAssembly());
// Add Swagger UI
services.AddApiDocumentation();

// Build the app and configure the request pipeline
var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseCors();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.UseApiDocumentation();
app.Run();