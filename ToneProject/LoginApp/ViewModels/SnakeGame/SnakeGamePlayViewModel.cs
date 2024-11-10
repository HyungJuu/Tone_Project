using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Threading;

namespace LoginApp.ViewModels.SnakeGame
{
    /// <summary>
    /// 스네이크 게임 플레이를 관리하는 뷰모델 클래스.
    /// </summary>
    public partial class SnakeGamePlayViewModel : ObservableObject
    {
        private readonly DashboardViewModel _dashboardViewModel;

        /// <summary>
        /// 대기시간 타이머
        /// </summary>
        private readonly DispatcherTimer _countdownTimer;

        /// <summary>
        /// 게임 타이머
        /// </summary>
        private readonly DispatcherTimer _gameTimer;

        /// <summary>
        /// 대기시간
        /// </summary>
        private TimeSpan _countdownTime;

        /// <summary>
        /// 게임 진행 시간
        /// </summary>
        private TimeSpan _gameTime;

        /// <summary>
        /// 현재 활성화 뷰모델
        /// </summary>
        [ObservableProperty]
        private object? _currentViewModel;

        /// <summary>
        /// 대기시간을 표시
        /// </summary>
        [ObservableProperty]
        private string _countdownDisplay;

        /// <summary>
        /// 게임진행시간을 표시
        /// </summary>
        [ObservableProperty]
        private string _timerDisplay;

        /// <summary>
        /// 게임 일시정지 여부
        /// </summary>
        [ObservableProperty]
        private bool isPaused = false;

        /// <summary>
        /// SnakeGamePlayViewModel 클래스의 생성자.
        /// 대기시간, 게임시간 관련 초기값 설정
        /// </summary>
        /// <param name="dashboardViewModel"></param>
        public SnakeGamePlayViewModel(DashboardViewModel dashboardViewModel)
        {
            _dashboardViewModel = dashboardViewModel;
            _countdownTime = TimeSpan.FromSeconds(3); // 대기시간 3초
            //_gameTime = TimeSpan.FromMinutes(10); // 게임시간 10분
            _gameTime = TimeSpan.FromSeconds(5); // 테스트용

            CountdownDisplay = _countdownTime.Seconds.ToString();
            TimerDisplay = _gameTime.ToString(@"mm\:ss");

            // 대기시간 초기화
            _countdownTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            _countdownTimer.Tick += UpdateCountdown;

            // 게임시간 초기화
            _gameTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            _gameTimer.Tick += UpdateGameTimer;
        }

        /// <summary>
        /// 게임 시작 -> 대기시간 카운트다운 시작 
        /// </summary>
        [RelayCommand]
        public void StartGame()
        {
            _countdownTimer.Start();
        }

        /// <summary>
        /// 게임 일시정지/재개.
        /// 재개 시 대기시간이 0 이상이면 대기시간 카운트다운 이어서, 아닐 경우 게임시간 카운트다운
        /// </summary>
        [RelayCommand]
        private void TogglePause()
        {
            IsPaused = !IsPaused;

            if (IsPaused)
            {
                _countdownTimer.Stop();
                _gameTimer.Stop();
            }
            else
            {
                if (_countdownTime > TimeSpan.Zero)
                {
                    _countdownTimer.Start();
                }
                else
                {
                    CountdownDisplay = string.Empty;
                    _gameTimer.Start();
                }
            }
        }

        /// <summary>
        /// 대기시간을 1초씩 감소시키며 업데이트.
        /// 대기시간 카운트다운이 끝나면 게임시간 카운트다운 시작.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateCountdown(object? sender, EventArgs e)
        {
            if (_countdownTime > TimeSpan.Zero)
            {
                _countdownTime = _countdownTime.Subtract(TimeSpan.FromSeconds(1));
                CountdownDisplay = _countdownTime.Seconds.ToString();
            }
            else
            {
                _countdownTimer.Stop();
                CountdownDisplay = string.Empty;
                StartMainTimer();
            }
        }

        /// <summary>
        /// 게임시간 타이머 시작
        /// </summary>
        private void StartMainTimer()
        {
            _gameTimer.Start();
        }

        /// <summary>
        /// 게임시간을 1초씩 감소시키며 업데이트.
        /// 게임시간 카운트다운이 끝나면 게임종료 화면으로 전환
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateGameTimer(object? sender, EventArgs e)
        {
            if (_gameTime > TimeSpan.Zero)
            {
                _gameTime = _gameTime.Subtract(TimeSpan.FromSeconds(1));
                TimerDisplay = _gameTime.ToString(@"mm\:ss");
            }
            else
            {
                _gameTimer.Stop();
                CurrentViewModel = new SnakeGameEndViewModel(_dashboardViewModel);
            }
        }
    }
}
