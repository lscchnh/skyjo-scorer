using Models.Domain;

namespace Models.Dto;

public class GameDto
{
    public int Number { get; set; }
    public DateTime PlayedOn { get; set; }
    public Dictionary<string, int> PlayerScores { get; set; } = [];

    public static GameDto FromDomain(Game game)
    {
        return new GameDto
        {
            Number = game.Number,
            PlayedOn = game.PlayedOn,
            PlayerScores = game.PlayerScores
                .OrderBy(ps => ps.Score)
                .ToDictionary(ps => ps.PlayerName, ps => ps.Score)
        };
    }
}