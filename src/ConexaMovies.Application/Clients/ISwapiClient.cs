using ConexaMovies.Application.Dtos;

namespace ConexaMovies.Application.Clients;

public interface ISwapiClient
{
    Task<IReadOnlyList<SwapiMovieDto>> GetFilmsAsync(CancellationToken ct = default);
}
