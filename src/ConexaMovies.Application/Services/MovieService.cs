using ConexaMovies.Application.Dtos;
using ConexaMovies.Domain.Entities;
using ConexaMovies.Domain.Exceptions;
using ConexaMovies.Domain.Interfaces;

namespace ConexaMovies.Application.Services;

public sealed class MovieService : IMovieService
{
    private readonly IMovieRepository _repo;
    private readonly IUnitOfWork _uow;

    public MovieService(IMovieRepository repo, IUnitOfWork uow)
    {
        _repo = repo;
        _uow = uow;
    }

    public async Task<int> CreateAsync(CreateMovieDto dto, CancellationToken ct = default)
    {
        if (await _repo.ExistsByEpisodeIdAsync(dto.EpisodeId, ct))
            throw new ConflictException($"EpisodeId {dto.EpisodeId} already exists.");

        var movie = new Movie
        {
            Title = dto.Title,
            EpisodeId = dto.EpisodeId,
            Director = dto.Director,
            ReleaseDate = dto.ReleaseDate
        };

        _repo.Add(movie);
        await _uow.SaveChangesAsync(ct);
        return movie.Id;
    }

    public async Task<IReadOnlyList<MovieBriefResponse>> ListAsync(int page, int pageSize, CancellationToken ct = default)
    {
        var movies = await _repo.ListAsync(page, pageSize, ct);
        return movies.Select(m => new MovieBriefResponse(
              m.Id,
              m.Title,
              m.EpisodeId,
              m.ReleaseDate
        )).ToList();
    }

    public async Task<MovieDetailResponse?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        var movie = await _repo.GetByIdAsync(id, ct);
        return movie is null
            ? null
            : new MovieDetailResponse(
                movie.Id,
                movie.Title,
                movie.EpisodeId,
                movie.Director,
                movie.ReleaseDate);
    }

    public async Task UpdateAsync(int id, UpdateMovieDto dto, CancellationToken ct = default)
    {
        var movie = await _repo.GetByIdAsync(id, ct)
                     ?? throw new NotFoundException($"Movie with id {id} not found.");

        if (!movie.EpisodeId.Equals(dto.EpisodeId, StringComparison.OrdinalIgnoreCase) &&
            await _repo.ExistsByEpisodeIdAsync(dto.EpisodeId, ct))
            throw new ConflictException($"EpisodeId {dto.EpisodeId} already exists.");

        movie.Title = dto.Title;
        movie.EpisodeId = dto.EpisodeId;
        movie.Director = dto.Director;
        movie.ReleaseDate = dto.ReleaseDate;

        _repo.Update(movie);
        await _uow.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(int id, CancellationToken ct = default)
    {
        var movie = await _repo.GetByIdAsync(id, ct)
                     ?? throw new NotFoundException($"Movie with id {id} not found.");

        _repo.Remove(movie);
        await _uow.SaveChangesAsync(ct);
    }
}
