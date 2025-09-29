using ConexaMovies.Domain.Entities;

namespace ConexaMovies.Domain.Interfaces;

public interface IMovieRepository : IRepository<Movie>
{
    Task<Movie?> GetByEpisodeIdAsync(string episodeId, CancellationToken ct = default);
    Task<bool> ExistsByEpisodeIdAsync(string episodeId, CancellationToken ct = default);
}
