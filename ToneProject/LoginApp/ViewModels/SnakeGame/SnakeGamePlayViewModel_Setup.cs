using CommunityToolkit.Mvvm.ComponentModel;
using LoginApp.Enums;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace LoginApp.ViewModels.SnakeGame;

/// <summary>
/// 스네이크 게임 플레이를 관리하는 뷰모델 클래스.
/// </summary>
/// <remarks>
/// Setup : 기본속성, 생성자 및 초기화<br/>
/// Control : 게임시작, 일시정지/재개, 색상변경<br/>
/// Movement : 스네이크 이동 및 방향 전환<br/>
/// Timers : 게임 타이머 및 대기시간 관리<br/>
/// GameOver : 경계 및 자가 충돌 감지, 게임 종료<br/>
/// Food : 먹이 생성 및 섭취
/// </remarks>
public partial class SnakeGamePlayViewModel : ObservableObject
{
    private readonly Random _random = new();
    private readonly DashboardViewModel _dashboardViewModel;

    /// <summary>
    /// 게임 타이머(대기시간 및 게임시간)
    /// </summary>
    private readonly DispatcherTimer _gameTimer;

    /// <summary>
    /// 스네이크 이동타이머
    /// </summary>
    private readonly DispatcherTimer _moveTimer;

    /// <summary>
    /// 마지막 먹이 이후 타이머
    /// </summary>
    private readonly DispatcherTimer _noFoodTimer;

    /// <summary>
    /// 게임 대기시간(게임 시작 전 카운트다운)
    /// </summary>
    private TimeSpan _readyTime;

    /// <summary>
    /// 게임 진행시간(게임 플레이 시간)
    /// </summary>
    private TimeSpan _playTime;

    /// <summary>
    /// 먹이 섭취 제한시간
    /// </summary>
    private TimeSpan _noFoodTime;

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

    [ObservableProperty]
    private string _noFoodTimeDisplay;

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

    /// <summary>
    /// 먹이 위치
    /// </summary>
    [ObservableProperty]
    private Point _foodLocation;

    /// <summary>
    /// 스네이크 색상
    /// </summary>
    [ObservableProperty]
    private Brush _snakeColor = Brushes.Green;

    /// <summary>
    /// 스네이크 및 먹이 크기
    /// </summary>
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

    public IEnumerable<SnakeSegment> SnakeSegments => [.. _snakeSegments];

    /// <summary>
    /// 생성자
    /// 타이머, 스네이크 및 먹이 위치 초기화
    /// </summary>
    /// <param name="dashboardViewModel"></param>
    public SnakeGamePlayViewModel(DashboardViewModel dashboardViewModel)
    {
        _dashboardViewModel = dashboardViewModel;

        _readyTime = TimeSpan.FromSeconds(3); // 대기시간 3초
        _playTime = TimeSpan.FromMinutes(1); // 게임 시간 1분
        _noFoodTime = TimeSpan.FromSeconds(10); // 먹이 섭취 제한시간 10초

        ReadyTimeDisplay = _readyTime.Seconds.ToString();
        PlayTimeDisplay = _playTime.ToString(@"mm\:ss");
        NoFoodTimeDisplay = $"({_noFoodTime:ss})";

        _snakeSegments = new LinkedList<SnakeSegment>();
        InitializeSnake();
        GenerateFood();

        _gameTimer = new DispatcherTimer
        {
            Interval = TimeSpan.FromSeconds(1)
        };
        _gameTimer.Tick += UpdateTimers;

        _moveTimer = new DispatcherTimer
        {
            Interval = TimeSpan.FromMilliseconds(100)
        };
        _moveTimer.Tick += OnGameTick;

        _noFoodTimer = new DispatcherTimer
        {
            Interval = TimeSpan.FromSeconds(1)
        };
        _noFoodTimer.Tick += UpdateNoFoodTimer;
    }

    /// <summary>
    /// 스네이크 초기 길이 및 위치 설정 메서드
    /// </summary>
    private void InitializeSnake()
    {
        int initialLength = 3;
        int startX = BoardWidth / 2;
        int startY = BoardHeight / 2;

        for (int i = 0; i < initialLength; i++)
        {
            _snakeSegments.AddLast(new SnakeSegment
            {
                X = startX - (i * SegmentSize),
                Y = startY
            });
        }
    }
}