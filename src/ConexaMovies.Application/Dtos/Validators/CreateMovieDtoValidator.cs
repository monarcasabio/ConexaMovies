using FluentValidation;

namespace ConexaMovies.Application.Dtos.Validators;

public sealed class CreateMovieDtoValidator : AbstractValidator<CreateMovieDto>
{
    public CreateMovieDtoValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(200);
        RuleFor(x => x.EpisodeId)
            .NotEmpty()
            .Matches(@"^(?=.*\d)[\dIVXLCDM]+$")
            .MaximumLength(10);
        RuleFor(x => x.Director).NotEmpty().MaximumLength(100);
    }
};
