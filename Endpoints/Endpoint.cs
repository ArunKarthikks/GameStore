using System;
using JobAPI.DB;
using JobAPI.Dtos;
using JobAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace JobAPI.Endpoints;

public static class Endpoint
{

    private static readonly List<Dtos.DT> games = [
    new (1, "PUBG", "Action-adventure", 59.99m, new DateOnly(2017, 3, 3)),
    new (2, "Mario", "Platformer", 59.99m, new DateOnly(2017, 10, 27)),
    new (3, "Red", "Action-adventure", 59.99m, new DateOnly(2018, 10, 26)),
];

public static RouteGroupBuilder MapGameEndpoints(this WebApplication app)
{
    var group = app.MapGroup("/games").WithTags("Games").WithParameterValidation();

// GET /games
group.MapGet("/", (GameContext context) => {
    var games = context.Games.Include(g => g.Genre).ToList();
    var gameDTOs = games.Select(g => new DT(g.Id, g.Name, g.Genre!.Name, g.Price, g.ReleaseDate)).ToList();
    return Results.Ok(gameDTOs);
});

// GET /games/{id}
group.MapGet("/{id}", (int id, GameContext context) => {
    var game = context.Games.Include(g => g.Genre).FirstOrDefault(g => g.Id == id);
    return game is null ? Results.NotFound() : Results.Ok(new DT(game.Id, game.Name, game.Genre!.Name, game.Price, game.ReleaseDate));
});

// Post /games
group.MapPost("/", (CreateDT createDT, GameContext context) => {
    Game game = new Game {
        Name = createDT.Name,
        Genre = context.Set<Genre>().Find(createDT.GenreID),
        GenreID = createDT.GenreID,
        Price = createDT.Price,
        ReleaseDate = createDT.ReleaseDate
    };
    //var game = new DT(games.Count + 1, createDT.Name, createDT.Genre, createDT.Price, createDT.ReleaseDate);
    context.Add(game);
    context.SaveChanges();
    //return Results.Created("/games/{id}", new { id = game.Id }, new DT(game.Id, game.Name, game.Genre!.Name, game.Price, game.ReleaseDate));
    return Results.Created("/games/{id}", new DT(game.Id, game.Name, game.Genre!.Name, game.Price, game.ReleaseDate));
   // return Results.CreatedAtRoute("/games/{id}", new { id = game.Id }, new DT(game.Id, game.Name, game.Genre!.Name, game.Price, game.ReleaseDate));
});

// PUT /games/{id}
group.MapPut("/{id}", (int id, UpdateDT updateDT) => {
    var game = games.FirstOrDefault(g => g.Id == id);
    if (game is null)
        return Results.NotFound();
    
    var updatedGame = game with {
        Name = updateDT.Name,
        Genre = updateDT.Genre,
        Price = updateDT.Price,
        ReleaseDate = updateDT.ReleaseDate
    };
    games[games.IndexOf(game)] = updatedGame;
    return Results.Ok(updatedGame);
});

// DELETE /games/{id}
group.MapDelete("/{id}", (int id) => {
    var game = games.FirstOrDefault(g => g.Id == id);
    if (game is null)
        return Results.NotFound();
    
    games.Remove(game);
    return Results.Content($"Game with id {id} deleted successfully")   ; 
});       

return group;
}
}