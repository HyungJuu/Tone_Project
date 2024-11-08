using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LoginApp.Models;
using System.Collections.ObjectModel;
using System.Windows;

namespace LoginApp.ViewModels.SnakeGame
{
    public partial class SnakeGameEndViewModel(DashboardViewModel dashboardViewModel) : ObservableObject
    {
        private readonly DashboardViewModel _dashboardViewModel = dashboardViewModel;

        public ObservableCollection<SnakeGameRecord> Scores { get; set; } = [];


        [ObservableProperty]
        private object? _currentViewModel;

        [ObservableProperty]
        private bool _isScoreVisible;

        [RelayCommand]
        public void ShowMyTopScore()
        {
            Scores.Clear();

            string? loggedInUserId = _dashboardViewModel.CurrentUserId ?? "Unknown";

            try
            {
                var topScores = ScoreService.GetTopScoresByUserId(loggedInUserId);

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
        public void StartAgain()
        {
            SnakeGamePlayViewModel snakeGamePlayViewModel = new(_dashboardViewModel);
            snakeGamePlayViewModel.StartGame();
            CurrentViewModel = snakeGamePlayViewModel;
        }
    }
}
