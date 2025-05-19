using Microsoft.EntityFrameworkCore;
using Models.Domain;
using Models.Dto;
using Repositories;

namespace Services;

public class PlayerService(GameDbContext gameDbContext) : IPlayerService
{
    public async Task<Player> Add(AddPlayerScore addPlayerScore)
    {
        var player = new Player
        {
            Name = addPlayerScore.PlayerName
        };

        var playerEntity = gameDbContext.Players.Add(player);

        await gameDbContext.SaveChangesAsync();

        return playerEntity.Entity;
    }

    public Task Delete(string name)
    {
        throw new NotImplementedException();
    }

    public Task<PlayerDto> Get(string name)
    {
        throw new NotImplementedException();
    }

    public async Task<List<PlayerDto>> GetAllWithStats(int? year)
    {
        List<Player> players = [.. (await gameDbContext.Players
            .Include(p => p.PlayerScores)
            .ThenInclude(ps => ps.Game)
            .ToListAsync())
            .Where(p => p.GetTotalGames(year) > 0)];

        List<PlayerDto> playerDtos = [];

        foreach (var player in players)
        {
            var playerDto = player.ToDto();
            playerDto.TotalGames = player.GetTotalGames(year);
            playerDto.AverageScore = await ComputeAverageScore(player, year);
            playerDtos.Add(playerDto);
        }

        return playerDtos;
    }

    private async Task<double> ComputeAverageScore(Player player, int? year)
    {
        int totalGames = player.GetTotalGames(year);

        if (totalGames == 0)
        {
            return 0;
        }

        float totalScore = player.GetTotalScore(year);

        int totalMaxGamesPlayed = (await gameDbContext.Players
            .Include(p => p.PlayerScores)
            .ThenInclude(ps => ps.Game)
            .OrderByDescending(p => p.PlayerScores.Where(ps => ps.Game != null && (year == null || ps.Game.PlayedOn.Year == year)).Count())
            .FirstOrDefaultAsync())
            ?.GetTotalGames(year) ?? 0;

        if (totalMaxGamesPlayed == 0)
        {
            return 0;
        }

        var playerGlobalAverageScore = totalGames > 0 ? totalScore / totalGames : 0;

        var finalAverageScore = playerGlobalAverageScore * (1 + Math.Log(totalMaxGamesPlayed)) / totalGames;

        return finalAverageScore;
    }
}
