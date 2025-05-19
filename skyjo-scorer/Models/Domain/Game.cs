using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Domain;

[Table("Game")]
public class Game
{
    public int Number { get; set; }

    public float CompletionCoefficient => 100 / (PlayerScores.Max(ps => ps.Score) == 0 ? 1 : PlayerScores.Max(ps => ps.Score));

    public DateTime PlayedOn { get; set; } = DateTime.UtcNow;

    public List<PlayerScore> PlayerScores { get; set; } = [];
}
