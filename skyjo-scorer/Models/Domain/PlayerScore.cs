using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Domain;

[Table("PlayerScore")]
public class PlayerScore
{
    public int Score { get; set; }

    public string PlayerName { get; set; } = string.Empty;
    public Player Player { get; set; } = null!;

    public int GameNumber { get; set; }
    public Game Game { get; set; } = null!;
}
