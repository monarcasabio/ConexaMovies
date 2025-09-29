namespace ConexaMovies.Application.Dtos;

public sealed record SwapiMovieDto(
    string Title,
    int Episode_Id,
    string Director,
    string Release_Date);
