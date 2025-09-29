using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ConexaMovies.Application.Services;

public sealed class SwapiBackgroundService : BackgroundService
{
    private readonly IServiceProvider _services;
    private readonly ILogger<SwapiBackgroundService> _log;
    private readonly TimeSpan _period = TimeSpan.FromHours(6);

    public SwapiBackgroundService(IServiceProvider services, ILogger<SwapiBackgroundService> log)
    {
        _services = services;
        _log = log;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var timer = new PeriodicTimer(_period);
        do
        {
            await SyncAsync(stoppingToken);
        } while (await timer.WaitForNextTickAsync(stoppingToken));
    }

    private async Task SyncAsync(CancellationToken ct)
    {
        try
        {
            using var scope = _services.CreateScope();
            var sync = scope.ServiceProvider.GetRequiredService<ISwapiSyncService>();
            var count = await sync.SyncAsync(ct);
            _log.LogInformation("Background Swapi-sync executed. New movies: {Count}", count);
        }
        catch (Exception ex)
        {
            _log.LogError(ex, "Background Swapi-sync failed");
        }
    }
}
