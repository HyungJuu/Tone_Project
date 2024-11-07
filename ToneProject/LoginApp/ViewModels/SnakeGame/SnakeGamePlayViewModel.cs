using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Threading;

namespace LoginApp.ViewModels.SnakeGame
{
    public partial class SnakeGamePlayViewModel : ObservableObject
    {
        private readonly SignInSuccessViewModel _signInSuccessViewModel;

        private readonly DispatcherTimer _countdownTimer;
        private readonly DispatcherTimer _gameTimer;

        private TimeSpan _countdownTime;
        private TimeSpan _gameTime;

        [ObservableProperty]
        private object? _currentViewModel;

        [ObservableProperty]
        private string _countdownDisplay;

        [ObservableProperty]
        private string _timerDisplay;

        [ObservableProperty]
        private bool isPaused = false;

        public SnakeGamePlayViewModel(SignInSuccessViewModel signInSuccessViewModel)
        {
            _signInSuccessViewModel = signInSuccessViewModel;
            _countdownTime = TimeSpan.FromSeconds(3); // 3초 카운트다운
            //_gameTime = TimeSpan.FromMinutes(1); // 10분 타이머
            _gameTime = TimeSpan.FromSeconds(5); // 테스트용

            CountdownDisplay = _countdownTime.Seconds.ToString();
            TimerDisplay = _gameTime.ToString(@"mm\:ss");

            _countdownTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            _countdownTimer.Tick += UpdateCountdown;

            _gameTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            _gameTimer.Tick += UpdateGameTimer;
        }

        /// <summary>
        /// 게임 시작
        /// </summary>
        [RelayCommand]
        public void StartGame()
        {
            _countdownTime = TimeSpan.FromSeconds(3);
            CountdownDisplay = _countdownTime.Seconds.ToString();
            _countdownTimer.Start();
        }

        /// <summary>
        /// 게임 일시정지/재개
        /// </summary>
        [RelayCommand]
        private void TogglePause()
        {
            IsPaused = !IsPaused;
        }

        private void UpdateCountdown(object? sender, EventArgs e)
        {
            if (IsPaused) return;

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

        // 게임 타이머 시작
        private void StartMainTimer()
        {
            TimerDisplay = _gameTime.ToString(@"mm\:ss");
            _gameTimer.Start();
        }

        private void UpdateGameTimer(object? sender, EventArgs e)
        {
            if (IsPaused) return;

            if (_gameTime > TimeSpan.Zero)
            {
                _gameTime = _gameTime.Subtract(TimeSpan.FromSeconds(1));
                TimerDisplay = _gameTime.ToString(@"mm\:ss");
            }
            else
            {
                _gameTimer.Stop();
                CurrentViewModel = new SnakeGameEndViewModel(_signInSuccessViewModel);
            }
        }
    }
}
