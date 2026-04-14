using FluentValidation;

namespace GameStore.Business.Dtos.Validations;

public class CreateGameDtoValidation : AbstractValidator<CreateGameDto>
{
    public CreateGameDtoValidation()
    {
        RuleFor(g => g.Name)
            .NotEmpty()
            .WithMessage("Name is required.")
            .Length(2, 100)
            .WithMessage("Name must be between {MinLength} and {MaxLength} characters.");

        RuleFor(g => g.Genre)
            .NotEmpty()
            .WithMessage("Genre is required.")
            .Length(2, 50)
            .WithMessage("Genre must be between {MinLength} and {MaxLength} characters.");

        RuleFor(g => g.Price)
            .NotEmpty()
            .GreaterThan(0)
            .WithMessage("Price must be greater than zero.");

        RuleFor(g => g.ReleaseDate)
            .NotEmpty()
            .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today))
            .WithMessage("Release date cannot be in the future.");
    }
}