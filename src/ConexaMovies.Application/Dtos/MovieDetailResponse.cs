namespace ConexaMovies.Application.Dtos;

public sealed record MovieDetailResponse(
    int Id,
    string Title,
    string EpisodeId,
    string Director,
    string ReleaseDate);