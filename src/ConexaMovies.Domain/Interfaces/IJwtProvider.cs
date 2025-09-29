using System.Security.Claims;

namespace ConexaMovies.Domain.Interfaces;

public interface IJwtProvider
{
    string GenerateToken(IEnumerable<Claim> claims);
}
