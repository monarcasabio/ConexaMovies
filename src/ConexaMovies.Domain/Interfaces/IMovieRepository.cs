using ConexaMovies.Domain.Entities;

namespace ConexaMovies.Domain.Interfaces;

public interface IMovieRepository : IRepository<Movie>
{
    Task<bool> ExistsByEpisodeIdAsync(string episodeId, CancellationToken ct = default);
    Task<IReadOnlyList<Movie>> ListAsync(int page, int pageSize, CancellationToken ct = default);
}
