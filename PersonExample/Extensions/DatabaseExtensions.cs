using Microsoft.EntityFrameworkCore;
using PersonExample.Data;

namespace PersonExample.Extensions;

public static class DatabaseExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<PersonDbContext>(options =>
            options.UseNpgsql(connectionString));

        return services;
    }

    public static void ApplyMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<PersonDbContext>();
        db.Database.Migrate();
    }
}
