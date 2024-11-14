using CommunityToolkit.Mvvm.Input;
using LoginApp.Enums;
using System.Windows.Media;

namespace LoginApp.ViewModels.SnakeGame
{
    public partial class SnakeGamePlayViewModel
    {
        /// <summary>
        /// 게임 시작 메서드(대기시간 카운트다운 시작)
        /// </summary>
        [RelayCommand]
        public void StartGame()
        {
            _isWaiting = true;
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
            }
            else if (_isWaiting)
            {
                _gameTimer.Start();
            }
            else
            {
                _moveTimer.Start();
                _gameTimer.Start();
            }
        }

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
        /// 스네이크 색상 랜덤변경 메서드
        /// </summary>
        public void ChangeSnakeColor()
        {
            byte GenerateBrightColor() => (byte)_random.Next(100, 256); // 밝은 색상
            var newColor = new SolidColorBrush(Color.FromRgb(
                r: GenerateBrightColor(),
                g: GenerateBrightColor(),
                b: GenerateBrightColor()
            ));

            foreach (var segment in _snakeSegments)
            {
                segment.SnakeColor = newColor;
            }
            OnPropertyChanged(nameof(SnakeSegments));
        }
    }
}
