using ConexaMovies.Application.Security;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace ConexaMovies.WebApi.Security;

public sealed class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _accessor;
    public CurrentUserService(IHttpContextAccessor accessor) => _accessor = accessor;

    public int? GetUserId() =>
        int.TryParse(_accessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier), out var id)
            ? id
            : null;
}
