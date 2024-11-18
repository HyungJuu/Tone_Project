using CommunityToolkit.Mvvm.Input;
using LoginApp.Enums;
using System.Windows.Media;

namespace LoginApp.ViewModels.SnakeGame;

public partial class SnakeGamePlayViewModel
{
    /// <summary>
    /// 스네이크 이동 방향 전환 메서드
    /// </summary>
    /// <param name="newDirection">새로운 이동 방향</param>
    [RelayCommand]
    public void Move(Direction newDirection)
    {
        if (newDirection != GetOppositeDirection(_currentDirection))
        {
            _currentDirection = newDirection;
        }
    }

    /// <summary>
    /// 현재 진행방향과 반대방향을 반환하는 메서드
    /// </summary>
    /// <param name="direction">현재 방향</param>
    /// <returns>반대방향</returns>
    /// <exception cref="InvalidOperationException"></exception>
    private static Direction GetOppositeDirection(Direction direction) => direction switch
    {
        Direction.Up => Direction.Down,
        Direction.Down => Direction.Up,
        Direction.Left => Direction.Right,
        Direction.Right => Direction.Left,
        _ => throw new InvalidOperationException()
    };

    /// <summary>
    /// 게임 타이머가 흐를 때마다 호출되는 메서드 -> 스네이크 이동 처리
    /// </summary>
    private void OnGameTick(object? sender, EventArgs e)
    {
        MoveSnake();
    }

    /// <summary>
    /// 스네이크 이동 메서드
    /// </summary>
    /// <remarks>
    /// 현재 방향에 따라 스네이크 머리 위치 업데이트 및 충돌 감지
    /// </remarks>
    private void MoveSnake()
    {
        SnakeSegment? head = _snakeSegments.FirstOrDefault();
        if (head == null) return;

        int newX = head.X;
        int newY = head.Y;
        Brush newColor = head.SnakeColor;

        // 현재 방향에 따라 머리 위치 조정
        switch (_currentDirection)
        {
            case Direction.Up:
                newY -= SegmentSize; break;
            case Direction.Right:
                newX += SegmentSize; break;
            case Direction.Down:
                newY += SegmentSize; break;
            case Direction.Left:
                newX -= SegmentSize; break;
        }

        // 새로운 머리 생성
        SnakeSegment newHead = new()
        {
            X = newX,
            Y = newY,
            SnakeColor = newColor
        };

        // 경계 및 자가 충돌 감지
        if (IsOutOfBounds(newHead) || IsCollidingWithSelf(newHead))
        {
            GameOver(false);
            return;
        }

        _snakeSegments.AddFirst(newHead);
        if (newHead.X == FoodLocation.X && newHead.Y == FoodLocation.Y)
        {
            EatFood();
        }
        else
        {
            _snakeSegments.RemoveLast(); // 먹이를 먹지 않은 경우에만 꼬리 제거
        }

        // 스네이크 이동 UI 업데이트
        OnPropertyChanged(nameof(SnakeSegments));
    }
}