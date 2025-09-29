using global::ConexaMovies.Application.Dtos;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ConexaMovies.Application.Clients;

public sealed class SwapiClient : ISwapiClient
{
    private readonly HttpClient _http;

    public SwapiClient(HttpClient http)
    {
        _http = http;
        // Asignamos la URL base de la API de Star Wars
        _http.BaseAddress = new Uri("https://swapi.dev/api/");
    }

    public async Task<IReadOnlyList<SwapiMovieDto>> GetFilmsAsync(CancellationToken ct = default)
    {
        // Hacemos la llamada al endpoint "films/"
        var response = await _http.GetFromJsonAsync<SwapiFilmsResponse>("films/", ct);

        // La API de SWAPI devuelve un objeto con una propiedad "results"
        return response?.Results ?? new List<SwapiMovieDto>();
    }
}

// Clase auxiliar para que el deserializador de JSON entienda la respuesta de la API
file sealed record SwapiFilmsResponse(
    [property: JsonPropertyName("results")] IReadOnlyList<SwapiMovieDto> Results);
