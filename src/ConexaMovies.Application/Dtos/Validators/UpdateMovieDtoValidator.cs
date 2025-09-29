using FluentValidation;

namespace ConexaMovies.Application.Dtos.Validators;

public sealed class UpdateMovieDtoValidator : AbstractValidator<UpdateMovieDto>
{
    public UpdateMovieDtoValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(200);
        RuleFor(x => x.EpisodeId).NotEmpty().MaximumLength(10);
        RuleFor(x => x.Director).NotEmpty().MaximumLength(100);
    }
}
