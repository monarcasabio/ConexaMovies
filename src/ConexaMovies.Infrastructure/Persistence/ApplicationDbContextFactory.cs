using ConexaMovies.Application.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ConexaMovies.Infrastructure.Persistence;

// Implementación de ICurrentUserService para tiempo de diseño
public class DesignTimeCurrentUserService : ICurrentUserService
{
    public int? GetUserId() => null;
}

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        // Obtener la ruta a la WebApi para leer el appsettings.json
        var basePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\ConexaMovies.WebApi"));

        var configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json")
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

        var connectionString = configuration.GetConnectionString("Default");

        optionsBuilder.UseSqlite(connectionString);

        // Pasamos una implementación "falsa" de ICurrentUserService que no hace nada
        return new ApplicationDbContext(optionsBuilder.Options, new DesignTimeCurrentUserService());
    }
}
