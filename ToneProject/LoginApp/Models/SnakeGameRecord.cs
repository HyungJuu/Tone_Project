namespace LoginApp.Models;

public partial class SnakeGameRecord
{
    public string UserId { get; set; } = null!;

    public DateOnly PlayedDate { get; set; }

    public bool GameClear { get; set; }

    public int PlayTime { get; set; }

    public int Score { get; set; }

    public int Id { get; set; }

    public virtual UserInfo User { get; set; } = null!;
}
