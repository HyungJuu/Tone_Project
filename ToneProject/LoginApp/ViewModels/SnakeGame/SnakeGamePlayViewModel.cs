using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Threading;

namespace LoginApp.ViewModels.SnakeGame
{
    public partial class SnakeGamePlayViewModel : ObservableObject
    {
        private readonly DispatcherTimer _countdownTimer;
        private readonly DispatcherTimer _gameTimer;
        private TimeSpan _countdownTime;
        private TimeSpan _gameTime;

        public SnakeGamePlayViewModel()
        {
            _countdownTime = TimeSpan.FromSeconds(3); // 3초 카운트다운
            _gameTime = TimeSpan.FromMinutes(10); // 10분 타이머

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

        [ObservableProperty]
        private string countdownDisplay;

        [ObservableProperty]
        private string timerDisplay;

        [ObservableProperty]
        private bool isPaused;

        // 게임 시작
        public void StartGame()
        {
            _countdownTime = TimeSpan.FromSeconds(3);
            CountdownDisplay = _countdownTime.Seconds.ToString();
            IsPaused = false; // 게임 시작 시 일시정지 해제
            _countdownTimer.Start();
        }

        // 일시정지/재개
        [RelayCommand]
        public void TogglePause()
        {
            IsPaused = !IsPaused; // IsPaused 속성의 값을 변경
        }

        // 카운트다운 타이머의 Tick 이벤트
        private void UpdateCountdown(object? sender, EventArgs e)
        {
            if (IsPaused) return;

            if (_countdownTime > TimeSpan.Zero)
            {
                _countdownTime = _countdownTime.Subtract(TimeSpan.FromSeconds(1)); // 1초씩 뺌
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

        // 게임 타이머의 Tick 이벤트
        private void UpdateGameTimer(object? sender, EventArgs e)
        {
            if (IsPaused) return; // 일시정지 시 종료

            if (_gameTime > TimeSpan.Zero)
            {
                _gameTime = _gameTime.Subtract(TimeSpan.FromSeconds(1)); // 1초씩 뺌
                TimerDisplay = _gameTime.ToString(@"mm\:ss");
            }
            else
            {
                _gameTimer.Stop();
                TimerDisplay = "성공!";
            }
        }
    }
}
