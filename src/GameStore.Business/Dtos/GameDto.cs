namespace GameStore.Business.Dtos;

public record GameDto(
    Guid Id,
    string Name,
    string Genre,
    decimal Price,
    DateOnly ReleaseDate
);