using ConexaMovies.Application.Security;
using ConexaMovies.Domain.Entities;
using ConexaMovies.Domain.Enums;
using ConexaMovies.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ConexaMovies.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IUnitOfWork
{
    private readonly ICurrentUserService _currentUser;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
                                ICurrentUserService currentUser) : base(options)
    {
        _currentUser = currentUser;
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<Movie> Movies => Set<Movie>();

    public override async Task<int> SaveChangesAsync(CancellationToken ct = default)
    {
        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        {
            var userId = _currentUser.GetUserId();
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    entry.Entity.CreatedBy = userId;
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                    entry.Entity.UpdatedBy = userId;
                    break;
            }
        }
        return await base.SaveChangesAsync(ct);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        // Seed roles
        builder.Entity<Role>().HasData(
            new Role { Id = (byte)UserRole.Regular, Name = "Regular" },
            new Role { Id = (byte)UserRole.Admin, Name = "Admin" }
        );
        base.OnModelCreating(builder);
    }
}
