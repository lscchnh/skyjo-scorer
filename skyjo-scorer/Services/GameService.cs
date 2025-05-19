using Microsoft.EntityFrameworkCore;
using Models.Domain;
using Models.Dto;
using Repositories;

namespace Services;

public class GameService(GameDbContext gameDbContext, IPlayerService playerService) : IGameService
{
    public async Task<List<GameDto>> GetAll(int? year)
    {
        List<Game> games = [];

        var query = gameDbContext.Games
        .Include(g => g.PlayerScores)
        .ThenInclude(ps => ps.Player)
        .AsQueryable();

        if (year is not null)
        {
            query = query.Where(g => g.PlayedOn.Year == year);
        }

        games = await query.ToListAsync();

        return [.. games.Select(GameDto.FromDomain)];
    }

    public async Task Add(AddGameRequest addGameRequest)
    {
        var game = new Game();

        foreach (var dto in addGameRequest.PlayerScores)
        {
            var player = await gameDbContext.Players.FindAsync(dto.PlayerName);

            player ??= await playerService.Add(dto);

            game.PlayerScores.Add(new PlayerScore
            {
                PlayerName = dto.PlayerName,
                Score = dto.Score,
                Player = player
            });
        }

        gameDbContext.Games.Add(game);

        await gameDbContext.SaveChangesAsync();
    }

    public async Task Delete(int number)
    {
        await gameDbContext.Games
            .Where(g => g.Number == number)
            .ExecuteDeleteAsync();
    }
}

