namespace ConexaMovies.Application.Dtos;

public sealed record CreateMovieDto(
    string Title,
    string EpisodeId,
    string Director,
    string ReleaseDate
);
