using Microsoft.EntityFrameworkCore;
using SpaceReservation.Application.Data;
using SpaceReservation.Application.Services;
using SpaceReservation.Domain.Entities;
using Microsoft.Extensions.Configuration;

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(_configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<ReservationService>();

        services.AddControllers();
        // Otros servicios necesarios
    }

    // Método Configure...
}