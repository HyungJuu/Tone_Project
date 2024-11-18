using System.Windows;
namespace LoginApp.ViewModels.SnakeGame;

public partial class SnakeGamePlayViewModel
{
    /// <summary>
    /// 새로운 먹이를 생성하는 메서드
    /// </summary>
    private void GenerateFood()
    {
        List<Point> possibleLocations = [];

        for (int x = 0; x < BoardWidth - SegmentSize; x += SegmentSize)
        {
            for (int y = 0; y < BoardHeight - SegmentSize; y += SegmentSize)
            {
                possibleLocations.Add(new Point(x + 7, y + 7));
            }
        }

        foreach (SnakeSegment segment in _snakeSegments) // 스네이크가 차지하고 있는 위치를 모두 제거
        {
            possibleLocations.RemoveAll(p => p.X == segment.X && p.Y == segment.Y);
        }

        if (possibleLocations.Count > 0)
        {
            FoodLocation = possibleLocations[_random.Next(possibleLocations.Count)];
        }
        else
        {
            GameOver();
        }

        OnPropertyChanged(nameof(FoodLocation));
    }

    /// <summary>
    /// 스네이크가 먹이를 먹었을 때 호출되는 메서드
    /// </summary>
    /// <remarks>
    /// 점수 1 증가, 스네이크 색상변경 및 새로운 먹이 생성.<br/>
    /// 먹이 섭취 제한시간 초기화
    /// </remarks>
    private void EatFood()
    {
        _noFoodTime = TimeSpan.FromSeconds(10);
        NoFoodTimeDisplay = $"({_noFoodTime:ss})";
        _noFoodTimer.Stop();
        _noFoodTimer.Start();

        Score++;
        ChangeSnakeColor();
        GenerateFood();
    }
}