using ConexaMovies.Application.Dtos;

namespace ConexaMovies.Application.Services;

public interface IUserService
{
    Task<TokenResponse> RegisterAsync(RegisterDto dto, CancellationToken ct = default);
    Task<TokenResponse> LoginAsync(LoginDto dto, CancellationToken ct = default);
}
