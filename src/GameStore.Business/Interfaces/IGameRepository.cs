using GameStore.Business.Dtos;

namespace GameStore.Business.Interfaces;

public interface IGameRepository : IRepository<Game>
{
    Task<IEnumerable<GameDto>> GetGamesByGenreAsync(string genre);
    Task<IEnumerable<GameDto>> GetGamesByPriceRangeAsync(decimal minPrice, decimal maxPrice);
    Task<IEnumerable<GameDto>> GetGamesReleasedAfterAsync(DateOnly releaseDate);
    Task<IEnumerable<GameDto>> GetGamesByNameAsync(string name);
    Task<IEnumerable<GameDto>> GetGameByDateRangeAsync(DateOnly startDate, DateOnly endDate);
}