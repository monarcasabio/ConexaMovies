namespace ConexaMovies.Domain.Entities;

public class Movie : AuditableEntity
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string EpisodeId { get; set; } = string.Empty;  
    public string Director { get; set; } = string.Empty;
    public string ReleaseDate { get; set; } = string.Empty;
}
