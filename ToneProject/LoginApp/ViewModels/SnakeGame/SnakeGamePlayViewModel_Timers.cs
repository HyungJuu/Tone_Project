namespace LoginApp.ViewModels.SnakeGame;

public partial class SnakeGamePlayViewModel
{
    /// <summary>
    /// 타이머를 1초씩 감소시키며 대기시간 및 게임시간을 업데이트.
    /// </summary>
    private void UpdateTimers(object? sender, EventArgs e)
    {
        if (_isWaiting)
        {
            if (_readyTime > TimeSpan.Zero)
            {
                _readyTime = _readyTime.Subtract(TimeSpan.FromSeconds(1));
                ReadyTimeDisplay = _readyTime.Seconds.ToString();
            }
            else
            {
                _isWaiting = false;
                ReadyTimeDisplay = string.Empty;
                _moveTimer.Start();
            }
        }
        else if (_playTime > TimeSpan.Zero)
        {
            _playTime = _playTime.Subtract(TimeSpan.FromSeconds(1));
            PlayTimeDisplay = _playTime.ToString(@"mm\:ss");
        }
        else
        {
            GameOver();
        }
    }
}