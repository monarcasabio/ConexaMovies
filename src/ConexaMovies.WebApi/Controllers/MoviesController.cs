using ConexaMovies.Application.Dtos;
using ConexaMovies.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConexaMovies.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MoviesController : ControllerBase
{
    private readonly IMovieService _service;
    public MoviesController(IMovieService service) => _service = service;

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> Create(
        [FromBody] CreateMovieDto dto,
        CancellationToken ct)
    {
        var id = await _service.CreateAsync(dto, ct);
        return Ok(new { id });
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<MovieBriefResponse>>> List(
    [FromQuery] int page = 1,
    [FromQuery] int pageSize = 10,
    CancellationToken ct = default)
    {
        if (page <= 0 || pageSize <= 0) return BadRequest("Page and pageSize must be positive.");
        var list = await _service.ListAsync(page, pageSize, ct);
        return Ok(list);
    }

    [HttpGet("{id:int}")]
    [Authorize(Roles = "Regular,Admin")]
    public async Task<ActionResult<MovieDetailResponse>> GetById(int id, CancellationToken ct)
    {
        var detail = await _service.GetByIdAsync(id, ct);
        return detail is null ? NotFound() : Ok(detail);
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> Update(
    int id,
    [FromBody] UpdateMovieDto dto,
    CancellationToken ct)
    {
        await _service.UpdateAsync(id, dto, ct);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> Delete(int id, CancellationToken ct)
    {
        await _service.DeleteAsync(id, ct);
        return NoContent();
    }
}
