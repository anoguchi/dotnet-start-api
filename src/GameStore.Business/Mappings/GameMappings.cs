using GameStore.Business.Dtos;

namespace GameStore.Business.Mappings;

public static class GameMappings
{
    public static GameDto ToDto(this Game game) => new GameDto
    (
        game.Id,
        game.Name,
        game.Genre,
        game.Price,
        game.ReleaseDate
    );
    
    public static Game ToGame(this CreateGameDto dto) => new Game
    {
        Name = dto.Name,
        Genre = dto.Genre,
        Price = dto.Price,
        ReleaseDate = dto.ReleaseDate
    };

    public static void UpdateFromDto(this Game game, UpdateGameDto dto)
    {
        game.Name = dto.Name;
        game.Genre = dto.Genre;
        game.Price = dto.Price;
        game.ReleaseDate = dto.ReleaseDate;
    }
}