using ConexaMovies.Application.Dtos;

namespace ConexaMovies.Application.Services;

public interface IMovieService
{
    Task<int> CreateAsync(CreateMovieDto dto, CancellationToken ct = default);
    Task<IReadOnlyList<MovieBriefResponse>> ListAsync(int page, int pageSize, CancellationToken ct = default);
    Task<MovieDetailResponse?> GetByIdAsync(int id, CancellationToken ct = default);
    Task UpdateAsync(int id, UpdateMovieDto dto, CancellationToken ct = default);
    Task DeleteAsync(int id, CancellationToken ct = default);
}
