namespace Models.Dto;
public class PlayerDto
{
    public string Name { get; set; } = string.Empty;
    public List<GameDto> Games { get; set; } = [];
    public double AverageScore { get; set; }
    public int TotalGames { get; set; }
}
