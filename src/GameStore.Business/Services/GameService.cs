using GameStore.Business.Dtos;
using GameStore.Business.Interfaces;

namespace GameStore.Business.Services;

public class GameService : BaseService, IGameService
{
    private readonly IGameRepository _gameRepository;

    public GameService(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }

    public async Task AddGameAsync(CreateGameDto newGame)
    {
        // Validar se a entidade é consistente

        // Validar se já não existe um jogo com o mesmo nome

        await _gameRepository.AddAsync(new Game
        {
            Id = Guid.NewGuid(),
            Name = newGame.Name,
            Genre = newGame.Genre,
            Price = newGame.Price,
            ReleaseDate = newGame.ReleaseDate
        });
    }

    public async Task UpdateGameAsync(Guid id, UpdateGameDto updateGame)
    {
        var existingGame = await _gameRepository.GetByIdAsync(id);
        if (existingGame == null)
        {
            throw new Exception("Game not found");
        }

        existingGame.Name = updateGame.Name;
        existingGame.Genre = updateGame.Genre;
        existingGame.Price = updateGame.Price;
        existingGame.ReleaseDate = updateGame.ReleaseDate;

        await _gameRepository.UpdateAsync(existingGame);
    }

    public async Task DeleteGameAsync(Guid id)
    {
        await _gameRepository.DeleteAsync(id);
    }

    public void Dispose()
    {
        _gameRepository?.Dispose();
    }
}