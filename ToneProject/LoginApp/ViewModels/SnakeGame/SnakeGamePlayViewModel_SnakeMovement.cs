using LoginApp.Enums;
using System.Windows;
using System.Windows.Media;

namespace LoginApp.ViewModels.SnakeGame
{
    public partial class SnakeGamePlayViewModel
    {
        /// <summary>
        /// 게임타이머가 흐를때마다 호출되는 메서드.
        /// 스네이크 이동 처리.
        /// </summary>
        private void OnGameTick(object? sender, EventArgs e)
        {
            MoveSnake();
        }

        /// <summary>
        /// 스네이크 이동 메서드.
        /// 방향에 따라 스네이크 머리 위치 업데이트 및 충돌 감지.
        /// </summary>
        private void MoveSnake()
        {
            var head = _snakeSegments.FirstOrDefault();
            var tail = _snakeSegments.LastOrDefault();

            if (head != null && tail != null)
            {
                int newX = head.X;
                int newY = head.Y;
                Brush newColor = head.SnakeColor;

                // 현재 방향에 따라 머리 위치 조정
                switch (_currentDirection)
                {
                    case Direction.Up:
                        newY -= SegmentSize;
                        break;
                    case Direction.Right:
                        newX += SegmentSize;
                        break;
                    case Direction.Down:
                        newY += SegmentSize;
                        break;
                    case Direction.Left:
                        newX -= SegmentSize;
                        break;
                }

                SnakeSegment newHead = new()
                {
                    X = newX,
                    Y = newY,
                    SnakeColor = newColor
                };

                // 경계 및 자가 충돌 감지
                if (IsOutOfBounds(newHead) || IsCollidingWithSelf(newHead))
                {
                    GameOver();
                    return;
                }

                _snakeSegments.AddFirst(newHead);

                // 스네이크가 먹이를 먹었을 경우
                if (newHead.X == FoodLocation.X && newHead.Y == FoodLocation.Y)
                {
                    EatFood();
                }
                else
                {
                    _snakeSegments.RemoveLast(); // 먹이를 먹지 않았을 경우(이동 중일 경우) 꼬리 제거
                }

                // 스네이크 이동 UI 업데이트
                OnPropertyChanged(nameof(SnakeSegments));
            }
            else
            {
                GameOver();
            }
        }

        /// <summary>
        /// 타이머를 1초씩 감소시키며 대기시간과 게임시간을 업데이트.
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

            if (!_isWaiting && _playTime > TimeSpan.Zero)
            {
                _playTime = _playTime.Subtract(TimeSpan.FromSeconds(1));
                PlayTimeDisplay = _playTime.ToString(@"mm\:ss");
            }
            else if (_playTime <= TimeSpan.Zero)
            {
                GameOver();
            }
        }

        /// <summary>
        /// 새로운 먹이를 생성하는 메서드
        /// </summary>
        private void GenerateFood()
        {
            // 가능한 모든 위치를 저장하는 리스트 생성
            var possibleLocations = new List<Point>();

            for (int x = 0; x < BoardWidth - SegmentSize; x += SegmentSize)
            {
                for (int y = 0; y < BoardHeight - SegmentSize; y += SegmentSize)
                {
                    possibleLocations.Add(new Point(x + 7, y + 7));
                }
            }

            // 스네이크가 차지하고 있는 위치를 모두 제거
            foreach (var segment in _snakeSegments)
            {
                possibleLocations.RemoveAll(p => p.X == segment.X && p.Y == segment.Y);
            }

            // 나머지 위치 중 랜덤으로 선택
            if (possibleLocations.Count > 0)
            {
                FoodLocation = possibleLocations[_random.Next(possibleLocations.Count)];
            }
            else
            {
                GameOver();
            }
        }

        /// <summary>
        /// 먹이를 먹었을 때 호출되는 메서드.
        /// 점수 1 증가, 스네이크 색상 변경 및 새로운 먹이 생성
        /// </summary>
        private void EatFood()
        {
            Score++;
            ChangeSnakeColor();
            GenerateFood();
        }

        /// <summary>
        /// 스네이크 머리가 경계를 벗어났는지 확인하는 메서드
        /// </summary>
        /// <param name="head">스네이크 머리</param>
        /// <returns>경계를 벗어났는지 여부 반환</returns>
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
        /// 스네이크 머리가 몸통과 충돌했는지 확인하는 메서드
        /// </summary>
        /// <param name="head">스네이크 머리</param>
        /// <returns>머리와 몸통 충돌 여부 반환</returns>
        private bool IsCollidingWithSelf(SnakeSegment head)
            => _snakeSegments.Any(segment => segment.X == head.X && segment.Y == head.Y);

        /// <summary>
        /// 게임 종료를 처리하는 메서드.
        /// 타이머, 스네이크를 멈추고 게임종료 화면 전환.
        /// </summary>
        private void GameOver()
        {
            _gameTimer.Stop();
            _moveTimer.Stop();
            CurrentViewModel = new SnakeGameEndViewModel(_dashboardViewModel);
        }
    }
}
