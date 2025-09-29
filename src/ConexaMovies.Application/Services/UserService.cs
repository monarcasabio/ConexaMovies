using System.Security.Claims;
using ConexaMovies.Application.Dtos;
using ConexaMovies.Domain.Entities;
using ConexaMovies.Domain.Enums;
using ConexaMovies.Domain.Interfaces;
using ConexaMovies.Domain.Exceptions;
using Microsoft.Extensions.Logging;

namespace ConexaMovies.Application.Services;

public sealed class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _hasher;
    private readonly IJwtProvider _jwt;

    public UserService(
        IUserRepository userRepository,
        IUnitOfWork unitOfWork,
        IPasswordHasher hasher,
        IJwtProvider jwt)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _hasher = hasher;
        _jwt = jwt;
    }

    public async Task<TokenResponse> RegisterAsync(RegisterDto dto, CancellationToken ct = default)
    {
        if (await _userRepository.ExistsByUsernameAsync(dto.Username, ct))
            throw new ConflictException("Username already exists.");

        var user = new User
        {
            Username = dto.Username,
            PasswordHash = _hasher.Hash(dto.Password),
            RoleId = (byte)UserRole.Regular
        };

        await _userRepository.AddAsync(user);          
        await _unitOfWork.SaveChangesAsync(ct);

        // Ahora que el usuario está guardado, podemos crear el token para un auto-login
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Role, UserRole.Regular.ToString()) 
        };

        var accessToken = _jwt.GenerateToken(claims);
        return new TokenResponse(accessToken);
    }

    public async Task<TokenResponse> LoginAsync(LoginDto dto, CancellationToken ct = default)
    {
        var user = await _userRepository.GetByUsernameWithRoleAsync(dto.Username, ct);

        if (user is null || !_hasher.Verify(dto.Password, user.PasswordHash))
            throw new UnauthorizedException("Invalid credentials");

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Role, user.Role.Name) 
        };

        var accessToken = _jwt.GenerateToken(claims);
        return new TokenResponse(accessToken);
    }
}
