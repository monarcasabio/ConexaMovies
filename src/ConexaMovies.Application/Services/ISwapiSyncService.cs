namespace ConexaMovies.Application.Services;

public interface ISwapiSyncService
{
    Task<int> SyncAsync(CancellationToken ct = default);
}
