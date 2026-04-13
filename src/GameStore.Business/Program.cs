using GameStore.Business.Dtos;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

List<GameDto> games =
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

const string GetGameById = "GetGameById";

// GET /games
app.MapGet("/api/games", () => games);


// GET /api/games/Guid
app.MapGet("/api/games/{id:Guid}", (Guid id) =>
{
    var gamefound = games.Find(game => game.Id == id);
    return gamefound is null ? Results.NotFound() : Results.Ok(gamefound);
}).WithName(GetGameById);

// POST /api/games
app.MapPost("/api/games", (CreateGameDto newGame) =>
{
    GameDto game = new(
        Guid.NewGuid(),
        newGame.Name,
        newGame.Genre,
        newGame.Price,
        newGame.ReleaseDate
    );
    games.Add(game);

    return Results.CreatedAtRoute(GetGameById, new { id = game.Id }, game);
});

// PUT /api/games/Guid
app.MapPut("/api/games/{id:Guid}", (Guid id, UpdateGameDto updatedGame) =>
{
    var index = games.FindIndex(game => game.Id == id);

    if (index == -1)
    {
        return Results.NotFound();
    }

    games[index] = new GameDto(
        id,
        updatedGame.Name,
        updatedGame.Genre,
        updatedGame.Price,
        updatedGame.ReleaseDate
    );
    return Results.NoContent();
});

// DELETE /api/games/Guid
app.MapDelete("/api/games/{id:Guid}", (Guid id) =>
{
    games.RemoveAll(game => game.Id == id);
    return Results.NoContent();
});

app.Run();