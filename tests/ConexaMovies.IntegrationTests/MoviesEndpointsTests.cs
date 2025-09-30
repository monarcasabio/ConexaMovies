using ConexaMovies.Application.Dtos;
using System.Net.Http.Json;
using System.Net;
using FluentAssertions;
using Xunit;

namespace ConexaMovies.IntegrationTests;

public class MoviesEndpointsTests : IClassFixture<ConexaMoviesFactory>
{
    private readonly HttpClient _client = new()
    {
        BaseAddress = new Uri("https://localhost:5001")
    };

    [Fact]
    public async Task Get_Movies_WithoutToken_Returns401()
    {
        var response = await _client.GetAsync("/api/movies");
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Post_Movie_InvalidDto_Returns400()
    {
        await AuthHelper.LoginAdminAsync(_client);
        var badDto = new { Title = "" };
        var response = await _client.PostAsJsonAsync("/api/movies", badDto);
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Post_Movie_ValidDto_Returns201()
    {
        await AuthHelper.LoginAdminAsync(_client);
        var dto = new CreateMovieDto("A New Hope", "4", "George Lucas", "1977-05-25");
        var response = await _client.PostAsJsonAsync("/api/movies", dto);
        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}
