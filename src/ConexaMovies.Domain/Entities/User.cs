﻿namespace ConexaMovies.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public byte RoleId { get; set; }

    public Role Role { get; set; } = null!;
}
