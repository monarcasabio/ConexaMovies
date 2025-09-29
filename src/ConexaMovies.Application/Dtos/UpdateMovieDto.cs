namespace ConexaMovies.Application.Dtos;

public sealed record UpdateMovieDto(
    string Title,
    string EpisodeId,
    string Director,
    string ReleaseDate);