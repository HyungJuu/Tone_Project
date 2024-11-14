using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LoginApp.Enums;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace LoginApp.ViewModels.SnakeGame
{
    /// <summary>
    /// 스네이크 게임 플레이를 관리하는 뷰모델 클래스.
    /// </summary>
    public partial class SnakeGamePlayViewModel : ObservableObject
    {
        private readonly DashboardViewModel _dashboardViewModel;
        private readonly Random _random = new();

        /// <summary>
        /// 게임 타이머(대기시간 및 게임시간)
        /// </summary>
        private readonly DispatcherTimer _gameTimer;

        /// <summary>
        /// 스네이크 이동타이머
        /// </summary>
        private readonly DispatcherTimer _moveTimer;

        /// <summary>
        /// 게임 대기시간(게임 시작 전 카운트다운)
        /// </summary>
        private TimeSpan _readyTime;

        /// <summary>
        /// 게임 진행시간(게임 플레이 시간)
        /// </summary>
        private TimeSpan _playTime;


        /// <summary>
        /// 게임 대기시간 표시
        /// </summary>
        [ObservableProperty]
        private string _readyTimeDisplay;

        /// <summary>
        /// 게임 진행시간 표시
        /// </summary>
        [ObservableProperty]
        private string _playTimeDisplay;

        /// <summary>
        /// 현재 활성화 뷰모델
        /// </summary>
        [ObservableProperty]
        private object? _currentViewModel;

        /// <summary>
        /// 게임 일시정지 여부
        /// </summary>
        [ObservableProperty]
        private bool isPaused = false;

        /// <summary>
        /// 게임 점수
        /// </summary>
        [ObservableProperty]
        private int _score = 0;

        /// <summary>
        /// 게임영역 너비
        /// </summary>
        [ObservableProperty]
        private int boardWidth = 525;

        /// <summary>
        /// 게임영역 높이
        /// </summary>
        [ObservableProperty]
        private int boardHeight = 375;

        [ObservableProperty]
        private Point _foodLocation;

        [ObservableProperty]
        private Brush _snakeColor = Brushes.Green;  // 기본 색상

        /// <summary>
        /// 스네이크 크기
        /// </summary>
        private readonly int SegmentSize = 15;


        private readonly int SegmentSize = 15;

        /// <summary>
        /// 스네이크 초기 방향 : 오른쪽
        /// </summary>
        private Direction _currentDirection = Direction.Right;

        /// <summary>
        /// 게임 대기 상태 여부
        /// </summary>
        private bool _isWaiting = true;

        /// <summary>
        /// 스네이크의 각 구간을 저장하는 연결 리스트
        /// </summary>
        private readonly LinkedList<SnakeSegment> _snakeSegments;

        public IEnumerable<SnakeSegment> SnakeSegments => _snakeSegments.ToList();

        /// <summary>
        /// SnakeGamePlayViewModel 클래스의 생성자.
        /// 대기시간, 게임시간 관련 초기값 설정
        /// </summary>
        /// <param name="dashboardViewModel"></param>
        public SnakeGamePlayViewModel(DashboardViewModel dashboardViewModel)
        {
            _dashboardViewModel = dashboardViewModel;
            _readyTime = TimeSpan.FromSeconds(3); // 대기시간 3초
            _playTime = TimeSpan.FromMinutes(1);  // 게임 시간 1분

            ReadyTimeDisplay = _readyTime.Seconds.ToString();
            PlayTimeDisplay = _playTime.ToString(@"mm\:ss");

            // 스네이크 초기 길이 및 시작 위치
            int initialLength = 3;
            int startX = BoardWidth / 2;
            int startY = BoardHeight / 2;

            _snakeSegments = new LinkedList<SnakeSegment>();

            Brush initialSnakeColor = _snakeColor ?? Brushes.Green;

            for (int i = 0; i < initialLength; i++)
            {
                _snakeSegments.AddLast(new SnakeSegment
                {
                    X = startX - (i * SegmentSize),
                    Y = startY
                });
            }

            GenerateFood();

            _gameTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            _gameTimer.Tick += UpdateTimers;

            // 스네이크 이동 타이머
            _moveTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(100)
            };
            _moveTimer.Tick += OnGameTick;
        }

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
            else
            {
                if (_isWaiting)
                {
                    _gameTimer.Start();
                }
                else
                {
                    _moveTimer.Start();
                    _gameTimer.Start();
                }
            }
        }

        /// <summary>
        /// 스네이크 방향 전환 메서드
        /// </summary>
        [RelayCommand]
        public void Move(Direction newDirection)
        {
            if (newDirection != GetOppositeDirection(_currentDirection))
            {
                _currentDirection = newDirection;
            }
        }


        private void OnGameTick(object? sender, EventArgs e)
        {
            MoveSnake();
        }

        /// <summary>
        /// 스네이크 이동 처리 메서드
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
                    SnakeColor = newColor // 기존 색상 유지
                };

                // 경계 및 자가 충돌 감지
                if (IsOutOfBounds(newHead) || IsCollidingWithSelf(newHead))
                {
                    GameOver();
                    return;
                }

                _snakeSegments.AddFirst(newHead);

                // 스네이크가 먹이를 먹었는지 확인
                if (newHead.X == FoodLocation.X && newHead.Y == FoodLocation.Y)
                {
                    EatFood();
                }
                else
                {
                    _snakeSegments.RemoveLast(); // 먹이를 먹지 않았을 경우만 꼬리 제거
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
        /// 타이머를 1초씩 감소시키며 업데이트.
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
        /// 현재 진행방향의 반대방향
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
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
        /// 경계 충돌 여부 메서드
        /// </summary>
        /// <param name="head"></param>
        /// <returns></returns>
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
        /// 자기자신 충돌 여부
        /// </summary>
        /// <param name="newHead"></param>
        /// <returns></returns>
        private bool IsCollidingWithSelf(SnakeSegment newHead)
            => _snakeSegments.Any(segment => segment.X == newHead.X && segment.Y == newHead.Y);

        /// <summary>
        /// 게임 종료 처리 메서드
        /// </summary>
        private void GameOver()
        {
            _gameTimer.Stop();
            _moveTimer.Stop();
            CurrentViewModel = new SnakeGameEndViewModel(_dashboardViewModel);
        }

        private void GenerateFood()
        {
            // 1. 가능한 모든 위치를 저장할 리스트 생성
            var possibleLocations = new List<Point>();

            for (int x = 0; x < BoardWidth - SegmentSize; x += SegmentSize)
            {
                for (int y = 0; y < BoardHeight - SegmentSize; y += SegmentSize)
                {
                    possibleLocations.Add(new Point(x + 7, y + 7));
                }
            }

            // 2. 스네이크가 차지하고 있는 위치를 모두 제거 (정확히 일치하는 좌표만 제거)
            foreach (var segment in _snakeSegments)
            {
                possibleLocations.RemoveAll(p => p.X == segment.X && p.Y == segment.Y);
            }

            // 3. 가능한 위치가 남아있다면 그 중 랜덤으로 선택
            if (possibleLocations.Count > 0)
            {
                // 먹이 위치를 스네이크와 동일한 그리드에 맞춰 설정
                FoodLocation = possibleLocations[_random.Next(possibleLocations.Count)];
            }
            else
            {
                // 더 이상 먹이를 생성할 공간이 없으면 게임 종료 처리
                GameOver();
            }

            // UI 업데이트
            OnPropertyChanged(nameof(FoodLocation));
        }

        /// <summary>
        /// 스네이크가 먹이를 먹을 때 호출되는 메서드.
        /// </summary>
        private void EatFood()
        {
            Score++;
            ChangeSnakeColor();
            GenerateFood();
        }

        // 먹이를 먹을 때마다 색상 변경
        public void ChangeSnakeColor()
        {
            // 밝은 색상만 생성 (각 RGB 값이 최소 100 이상)
            byte GenerateBrightColor()
            {
                return (byte)_random.Next(100, 256); // 최소 100부터 255까지
            }

            // 새로운 색상 생성
            var newColor = new SolidColorBrush(Color.FromRgb(
                r: GenerateBrightColor(),  // R (최소 100 이상)
                g: GenerateBrightColor(),  // G (최소 100 이상)
                b: GenerateBrightColor()   // B (최소 100 이상)
            ));

            // 스네이크의 모든 세그먼트 색상 변경
            foreach (var segment in _snakeSegments)
            {
                segment.SnakeColor = newColor;
            }

            // UI 업데이트
            OnPropertyChanged(nameof(SnakeColor));
        }

    }
}