namespace Models.Dto;

public class AddPlayerScore
{
    private string _playerName = string.Empty;

    public string PlayerName
    {
        get => _playerName;
        set => _playerName = value.ToLowerInvariant();
    }

    public int Score { get; set; }
}

