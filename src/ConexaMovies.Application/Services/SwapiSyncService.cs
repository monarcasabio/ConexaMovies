using ConexaMovies.Application.Clients;
using ConexaMovies.Domain.Entities;
using ConexaMovies.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace ConexaMovies.Application.Services;

public sealed class SwapiSyncService : ISwapiSyncService
{
    private readonly ISwapiClient _client;
    private readonly IMovieRepository _repo;
    private readonly IUnitOfWork _uow;
    private readonly ILogger<SwapiSyncService> _log;

    public SwapiSyncService(
        ISwapiClient client,
        IMovieRepository repo,
        IUnitOfWork uow,
        ILogger<SwapiSyncService> log)
    {
        _client = client;
        _repo = repo;
        _uow = uow;
        _log = log;
    }

    public async Task<int> SyncAsync(CancellationToken ct = default)
    {
        var films = await _client.GetFilmsAsync(ct);
        var upserted = 0;

        foreach (var f in films)
        {
            var episode = f.Episode_Id.ToString();
            if (await _repo.ExistsByEpisodeIdAsync(episode, ct)) continue;

            var movie = new Movie
            {
                Title = f.Title,
                EpisodeId = episode,
                Director = f.Director,
                ReleaseDate = f.Release_Date
            };

            _repo.Add(movie);
            upserted++;
        }

        if (upserted > 0) await _uow.SaveChangesAsync(ct);

        _log.LogInformation("Swapi sync completed. {Upserted} new movies", upserted);
        return upserted;
    }
}