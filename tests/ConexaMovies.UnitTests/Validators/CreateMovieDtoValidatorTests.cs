using FluentAssertions;
using ConexaMovies.Application.Dtos;
using ConexaMovies.Application.Dtos.Validators;

public class CreateMovieDtoValidatorTests
{
    private readonly CreateMovieDtoValidator _sut = new();

    [Theory]
    [InlineData("", "IV", "Director", "2025-01-01", "Title")]
    [InlineData("Title", "", "Director", "2025-01-01", "EpisodeId")]
    [InlineData("Title", "IV", "", "2025-01-01", "Director")]
    public void Should_Have_Error_When_Required_Field_Empty(string title, string episode, string director, string dateString, string field)
    {
        var dto = new CreateMovieDto(title, episode, director, dateString);

        var result = _sut.Validate(dto);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == field);
    }

    [Fact]
    public void Should_Pass_When_All_Valid()
    {
        var dto = new CreateMovieDto("A New Hope", "4", "George Lucas", "1977-05-25");

        var result = _sut.Validate(dto);

        result.IsValid.Should().BeTrue();
    }
}
