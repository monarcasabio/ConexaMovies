using ConexaMovies.Domain.Entities;
using ConexaMovies.Domain.Interfaces;
using ConexaMovies.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

public sealed class MovieRepository : IMovieRepository
{
    private readonly ApplicationDbContext _db;
    public MovieRepository(ApplicationDbContext db) => _db = db;

    public async Task<IEnumerable<Movie>> GetAllAsync(CancellationToken ct = default)
        => await _db.Movies.ToListAsync(ct);

    public Task<Movie?> GetByIdAsync(int id, CancellationToken ct = default)
        => _db.Movies.SingleOrDefaultAsync(m => m.Id == id, ct);

    public void Add(Movie entity) => _db.Movies.Add(entity);
    public void Update(Movie entity) => _db.Movies.Update(entity);
    public void Remove(Movie entity) => _db.Movies.Remove(entity);

    public Task<bool> ExistsByEpisodeIdAsync(string episodeId, CancellationToken ct = default)
        => _db.Movies.AnyAsync(m => m.EpisodeId == episodeId, ct);

    public async Task<IReadOnlyList<Movie>> ListAsync(int page, int pageSize, CancellationToken ct = default)
    {
        return await _db.Movies
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(ct);
    }
}
