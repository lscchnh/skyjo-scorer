using Models.Dto;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Domain;

[Table("Player")]
public class Player
{
    public string Name { get; set; } = string.Empty;

    public List<PlayerScore> PlayerScores { get; set; } = [];

    public PlayerDto ToDto()
    {

        return new PlayerDto
        {
            Name = Name,
            Games = [.. PlayerScores.Select(x => GameDto.FromDomain(x.Game))]
        };
    }

    public int GetTotalGames(int? year)
    {
        if (year is not null)
        {
            return PlayerScores.Count(ps => ps.Game != null && ps.Game.PlayedOn.Year == year);
        }
        else
        {
            return PlayerScores.Count;
        }
    }

    public float GetTotalScore(int? year)
    {
        if (year is not null)
        {
            return PlayerScores
                .Where(ps => ps.Game != null && ps.Game.PlayedOn.Year == year)
                .Sum(ps => ps.Score * ps.Game.CompletionCoefficient);
        }
        else
        {
            return PlayerScores.Sum(ps => ps.Score * ps.Game.CompletionCoefficient);
        }
    }
}
