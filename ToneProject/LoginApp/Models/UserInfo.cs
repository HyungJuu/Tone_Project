namespace LoginApp.Models;

public partial class UserInfo
{
    public string UserId { get; set; } = null!;

    public string Pwd { get; set; } = null!;

    public string Name { get; set; } = null!;

    public DateOnly Birth { get; set; }

    public string Gender { get; set; } = null!;

    public virtual ICollection<SnakeGameHistory> SnakeGameHistories { get; set; } = [];
}
