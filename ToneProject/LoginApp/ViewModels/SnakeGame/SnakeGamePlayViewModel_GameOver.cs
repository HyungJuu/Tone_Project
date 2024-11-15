using LoginApp.Enums;
namespace LoginApp.ViewModels.SnakeGame;

public partial class SnakeGamePlayViewModel
{
    /// <summary>
    /// 스네이크 머리가 경계를 벗어났는지 확인하는 메서드
    /// </summary>
    /// <param name="head">스네이크 머리</param>
    /// <returns>경계를 벗어났는지 결과 반환</returns>
    private bool IsOutOfBounds(SnakeSegment head)
    {
        int centerX = head.X + (SegmentSize / 2);
        int centerY = head.Y + (SegmentSize / 2);

        return _currentDirection switch
        {
            Direction.Up => centerY - (SegmentSize / 2) < 0,// 위쪽 경계
            Direction.Right => centerX + (SegmentSize / 2) > BoardWidth,// 오른쪽 경계
            Direction.Down => centerY + (SegmentSize / 2) > BoardHeight,// 아래쪽 경계
            Direction.Left => centerX - (SegmentSize / 2) < 0,// 왼쪽 경계
            _ => false,
        };
    }

    /// <summary>
    /// 스네이크 머리가 몸통에 부딪혔는지 확인하는 메서드
    /// </summary>
    /// <param name="head">스네이크 머리</param>
    /// <returns>머리와 몸통 충돌 여부 반환</returns>
    private bool IsCollidingWithSelf(SnakeSegment head)
    {
        return _snakeSegments.Any(segment => segment.X == head.X && segment.Y == head.Y);
    }

    /// <summary>
    /// 게임 종료를 처리하는 메서드
    /// </summary>
    /// <remarks>
    /// 타이머, 스네이크를 멈추고 게임종료 화면으로 전환
    /// </remarks>
    private void GameOver()
    {
        _gameTimer.Stop();
        _moveTimer.Stop();
        CurrentViewModel = new SnakeGameEndViewModel(_dashboardViewModel);
    }
}