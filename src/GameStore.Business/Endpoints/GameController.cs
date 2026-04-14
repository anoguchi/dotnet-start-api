using GameStore.Business.Dtos;
using GameStore.Business.Dtos.Validations;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Business.Endpoints;

[ApiController]
[Route("api/games")]
public class GameController : ControllerBase
{
    private static readonly List<GameDto> games =
    [
        new(
            Guid.Parse("d42cfd2c-72cd-4925-968c-2183a8f54536"),
            "Street Figther II",
            "Fighting",
            19.99M,
            new DateOnly(1992, 7, 15)),
        new(
            Guid.Parse("aaf5f775-3f63-4bc1-8dc0-864c872c1f27"),
            "Final Fantasy VII Rebirth",
            "RPG",
            69.99M,
            new DateOnly(2024, 2, 20)),
        new(
            Guid.Parse("1d04cd6c-deb3-4d72-9ed6-fc3fa59a3043"),
            "Astro Bot",
            "Platformer",
            22.45M,
            new DateOnly(2025, 11, 11)),
    ];

    [HttpGet]
    public ActionResult<List<GameDto>> GetGames() => Ok(games);

    [HttpGet("{id:Guid}")]
    public ActionResult<GameDto> GetGameById(Guid id)
    {
        var game = games.Find(g => g.Id == id);
        return game is null ? NotFound() : Ok(game);
    }

    [HttpPost]
    public ActionResult<GameDto> CreateGame(CreateGameDto newGame)
    {
        var validator = new CreateGameDtoValidation();
        var results = validator.Validate(newGame);

        if (!results.IsValid)
        {
            foreach (var failure in results.Errors)
            {
                Console.WriteLine("Property " + failure.PropertyName + " failed validation. Error was: " +
                                  failure.ErrorMessage);
            }

            return BadRequest("Invalid game data.");
        }

        var game = new GameDto(Guid.NewGuid(), newGame.Name, newGame.Genre, newGame.Price, newGame.ReleaseDate);
        games.Add(game);
        return CreatedAtAction(nameof(GetGameById), new { id = game.Id }, game);
    }

    [HttpPut("{id:Guid}")]
    public IActionResult UpdateGame(Guid id, UpdateGameDto updatedGame)
    {
        var validator = new UpdateGameDtoValidation();
        var results = validator.Validate(updatedGame);

        if (!results.IsValid)
        {
            foreach (var failure in results.Errors)
            {
                Console.WriteLine("Property " + failure.PropertyName + " failed validation. Error was: " +
                                  failure.ErrorMessage);
            }

            return BadRequest(results.Errors);
        }

        var index = games.FindIndex(g => g.Id == id);
        if (index == -1) return NotFound();

        games[index] = new GameDto(id, updatedGame.Name, updatedGame.Genre, updatedGame.Price, updatedGame.ReleaseDate);
        return NoContent();
    }

    [HttpDelete("{id:Guid}")]
    public IActionResult DeleteGame(Guid id)
    {
        games.RemoveAll(g => g.Id == id);
        return NoContent();
    }
}