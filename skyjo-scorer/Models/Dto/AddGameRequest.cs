namespace Models.Dto;

public class AddGameRequest
{
    public List<AddPlayerScore> PlayerScores { get; set; } = [];
}