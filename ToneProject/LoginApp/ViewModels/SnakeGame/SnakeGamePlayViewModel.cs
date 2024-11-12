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
        /// 게임 타이머
        /// </summary>
        private readonly DispatcherTimer _gameTimer;

        /// <summary>
        /// 게임 대기시간
        /// </summary>
        private TimeSpan _readyTime;

        /// <summary>
        /// 게임 진행시간
        /// </summary>
        private TimeSpan _playTime;

        /// <summary>
        /// 현재 활성화 뷰모델
        /// </summary>
        [ObservableProperty]
        private object? _currentViewModel;

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
        /// 게임 일시정지 여부
        /// </summary>
        [ObservableProperty]
        private bool isPaused = false;

        [ObservableProperty]
        private int _score = 0;

        /// <summary>
        /// SnakeGamePlayViewModel 클래스의 생성자.
        /// 대기시간, 게임시간 관련 초기값 설정
        /// </summary>
        /// <param name="dashboardViewModel"></param>
        public SnakeGamePlayViewModel(DashboardViewModel dashboardViewModel)
        {
            _dashboardViewModel = dashboardViewModel;
            _readyTime = TimeSpan.FromSeconds(3); // 대기시간 3초
            //_playTime = TimeSpan.FromMinutes(10); // 게임시간 10분
            _playTime = TimeSpan.FromSeconds(5); // 테스트용

            ReadyTimeDisplay = _readyTime.Seconds.ToString();
            PlayTimeDisplay = _playTime.ToString(@"mm\:ss");

            _gameTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            _gameTimer.Tick += UpdateTimers;
        }

        /// <summary>
        /// 게임 시작.
        /// 대기시간 카운트다운.
        /// </summary>
        [RelayCommand]
        public void StartGame()
        {
            _gameTimer.Start();
        }

        /// <summary>
        /// 게임 일시정지/재개.
        /// </summary>
        [RelayCommand]
        public void PauseGame()
        {
            IsPaused = !IsPaused;

            if (IsPaused)
            {
                _gameTimer.Stop();
            }
            else
            {
                _gameTimer.Start();
            }
        }

        /// <summary>
        /// 타이머를 1초씩 감소시키며 업데이트.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateTimers(object? sender, EventArgs e)
        {
            if (_readyTime > TimeSpan.Zero) // 대기시간이 남았을 경우
            {
                _readyTime = _readyTime.Subtract(TimeSpan.FromSeconds(1));
                ReadyTimeDisplay = _readyTime.Seconds.ToString();
            }
            else if (_playTime > TimeSpan.Zero) // 게임시간이 남았을 경우
            {
                _readyTime = TimeSpan.Zero;
                ReadyTimeDisplay = string.Empty;

                _playTime = _playTime.Subtract(TimeSpan.FromSeconds(1));
                PlayTimeDisplay = _playTime.ToString(@"mm\:ss");
            }
            else
            {
                _gameTimer.Stop();
                CurrentViewModel = new SnakeGameEndViewModel(_dashboardViewModel);
            }
        }

        /// <summary>
        /// 목표물을 먹었을 때 점수 증가.
        /// </summary>
        public void EatFood()
        {
            Score += 1;
        }
    }
}