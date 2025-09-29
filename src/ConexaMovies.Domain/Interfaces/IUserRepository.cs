using ConexaMovies.Domain.Entities;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(int id); 
    Task AddAsync(User user);

    Task<bool> ExistsByUsernameAsync(string username, CancellationToken ct = default);
    Task<User?> GetByUsernameWithRoleAsync(string username, CancellationToken ct = default);
}
