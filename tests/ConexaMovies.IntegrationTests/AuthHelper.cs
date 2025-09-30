using ConexaMovies.Application.Dtos;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace ConexaMovies.IntegrationTests;

public static class AuthHelper
{
    public static async Task LoginAdminAsync(HttpClient client)
    {
        var login = new { Username = "admin", Password = "Admin123!" };
        var response = await client.PostAsJsonAsync("/api/auth/login", login);
        response.EnsureSuccessStatusCode();
        var token = await response.Content.ReadFromJsonAsync<TokenResponse>();
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token?.AccessToken);
    }
}
