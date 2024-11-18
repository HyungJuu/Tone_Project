using CommunityToolkit.Mvvm.Input;
using System.Windows.Media;

namespace LoginApp.ViewModels.SnakeGame;

public partial class SnakeGamePlayViewModel
{
    /// <summary>
    /// 게임 시작 메서드(대기시간 카운트다운 시작)
    /// </summary>
    [RelayCommand]
    public void StartGame()
    {
        _gameTimer.Start();
    }

    /// <summary>
    /// 게임 일시정지/재개 매서드
    /// </summary>
    [RelayCommand]
    public void PauseGame()
    {
        IsPaused = !IsPaused;
        if (IsPaused)
        {
            _gameTimer.Stop();
            _moveTimer.Stop();
            _noFoodTimer.Stop();
        }
        else if (_isWaiting)
        {
            _gameTimer.Start();
        }
        else
        {
            _moveTimer.Start();
            _gameTimer.Start();
            _noFoodTimer.Start();
        }
    }

    /// <summary>
    /// 스네이크 색상 랜덤변경 메서드
    /// </summary>
    public void ChangeSnakeColor()
    {
        byte GenerateBrightColor() => (byte)_random.Next(100, 256); // 밝은 색상
        SolidColorBrush newColor = new(Color.FromRgb(
            r: GenerateBrightColor(),
            g: GenerateBrightColor(),
            b: GenerateBrightColor()
        ));

        foreach (SnakeSegment segment in _snakeSegments)
        {
            segment.SnakeColor = newColor;
        }
        OnPropertyChanged(nameof(SnakeSegments));
    }
}