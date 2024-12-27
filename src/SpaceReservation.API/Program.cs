using Microsoft.EntityFrameworkCore;
using SpaceReservation.Application.Data;
using SpaceReservation.Application.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        npgsqlOptions => npgsqlOptions
            .MigrationsAssembly("SpaceReservation.Application")
            .EnableRetryOnFailure()
    ));

// Registrar el servicio ReservationService
builder.Services.AddScoped<IReservationService, ReservationService>();

// Configurar Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "SpaceReservation API", Version = "v1" });
});

// Asegúrate de que AddControllers() esté presente
builder.Services.AddControllers();

var app = builder.Build();

// Test database connection
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    try
    {
        dbContext.Database.OpenConnection();
        dbContext.Database.CloseConnection();
        Console.WriteLine("Conexión a la base de datos exitosa.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error al conectar a la base de datos: {ex.Message}");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "SpaceReservation API v1");
    });
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
