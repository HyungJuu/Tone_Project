using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LoginApp.Models;
using System.Collections.ObjectModel;
using System.Windows;

namespace LoginApp.ViewModels.SnakeGame
{
    public partial class SnakeGameIntroViewModel(SignInSuccessViewModel signInSuccessViewModel) : ObservableObject
    {
        private readonly SignInSuccessViewModel _signInSuccessViewModel = signInSuccessViewModel;

        public ObservableCollection<SnakeGameRecord> Scores { get; set; } = [];

        [ObservableProperty]
        private object? _currentViewModel;

        [ObservableProperty]
        private bool _isScoreVisible;

        [RelayCommand]
        public void ShowMyTopScore()
        {
            Scores.Clear();

            string? loggedInUserId = _signInSuccessViewModel.CurrentUserId ?? "Unknown"; // 로그인된 사용자 아이디

            try
            {
                // ScoreService에서 점수 조회
                var topScores = ScoreService.GetTopScoresByUserId(loggedInUserId);

                // 조회된 점수를 Scores에 추가
                foreach (var record in topScores)
                {
                    Scores.Add(record);
                }

                IsScoreVisible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"데이터를 불러오는 중 오류가 발생했습니다: {ex.Message}");
            }
        }

        [RelayCommand]
        public void BackToMain()
        {
            _signInSuccessViewModel.BackToSelectGame();
        }

        [RelayCommand]
        public void StartSnakeGame()
        {
            SnakeGamePlayViewModel snakeGamePlayViewModel = new(_signInSuccessViewModel);
            snakeGamePlayViewModel.StartGame();  // 게임을 시작
            CurrentViewModel = snakeGamePlayViewModel;
        }
    }
}
