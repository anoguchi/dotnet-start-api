using GameStore.Business.Dtos;

namespace GameStore.Business.Interfaces;

public interface IGameService : IDisposable
{
    Task AddGameAsync(CreateGameDto newGame);
    Task UpdateGameAsync(Guid id, UpdateGameDto updateGame);
    Task DeleteGameAsync(Guid id);
}