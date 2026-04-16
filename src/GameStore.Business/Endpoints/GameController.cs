using GameStore.Business.Dtos;
using GameStore.Business.Dtos.Validations;
using GameStore.Business.Mappings;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Business.Endpoints;

[ApiController]
[Route("api/games")]
public class GameController : ControllerBase
{
    private static readonly List<Game>
        Games =
        [
            new Game
            {
                Name = "The Legend of Zelda: Breath of the Wild",
                Genre = "Action-adventure",
                Price = 59.99m,
                ReleaseDate = new DateOnly(2017, 3, 3)
            },

            new Game
            {
                Name = "God of War",
                Genre = "Action-adventure",
                Price = 49.99m,
                ReleaseDate = new DateOnly(2018, 4, 20)
            },

            new Game
            {
                Name = "Red Dead Redemption 2",
                Genre = "Action-adventure",
                Price = 39.99m,
                ReleaseDate = new DateOnly(2018, 10, 26)
            }
        ];

    [HttpGet]
    public ActionResult<List<GameDto>> GetGames()
    {
        return Ok(Games.Select(g => g.ToDto()).ToList());
    }

    [HttpGet("{id:Guid}")]
    public ActionResult<GameDto> GetGameById(Guid id)
    {
        var game = Games.Find(g => g.Id == id);
        if (game is null) return NotFound();

        return Ok(game.ToDto());
    }

    [HttpPost]
    public ActionResult<GameDto> CreateGame(CreateGameDto newGame)
    {
        var validator = new CreateGameDtoValidation();
        var results = validator.Validate(newGame);

        // if (!results.IsValid)
        // {
        //     foreach (var failure in results.Errors)
        //     {
        //         Console.WriteLine("Property " + failure.PropertyName + " failed validation. Error was: " +
        //                           failure.ErrorMessage);
        //     }
        //
        //     return BadRequest("Invalid game data.");
        // }

        // if (!results.IsValid)
        //     return BadRequest(results.Errors.Select(e => new { e.PropertyName, e.ErrorMessage }));
        if (!results.IsValid)
            return BadRequest(results.Errors);

        var game = newGame.ToGame();
        Games.Add(game);
        return CreatedAtAction(nameof(GetGameById), new { id = game.Id }, game.ToDto());
    }

    [HttpPut("{id:Guid}")]
    public IActionResult UpdateGame(Guid id, UpdateGameDto updatedGame)
    {
        var validator = new UpdateGameDtoValidation();
        var results = validator.Validate(updatedGame);

        if (!results.IsValid)
            return BadRequest(results.Errors);

        var game = Games.Find(g => g.Id == id);
        if (game is null) return NotFound();

        game.UpdateFromDto(updatedGame);
        return NoContent();
        
    }

    [HttpDelete("{id:Guid}")]
    public IActionResult DeleteGame(Guid id)
    {
        Games.RemoveAll(g => g.Id == id);
        return NoContent();
    }
}