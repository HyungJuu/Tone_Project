using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LoginApp.Models;
using System.Collections.ObjectModel;
using System.Windows;

namespace LoginApp.ViewModels.SnakeGame
{
    /// <summary>
    /// 게임 종료 후 성적조회 및 재시작 기능을 제공하는 뷰모델 클래스
    /// </summary>
    public partial class SnakeGameEndViewModel : ObservableObject
    {
        private readonly DashboardViewModel _dashboardViewModel;

        /// <summary>
        /// 사용자의 게임 기록을 저장하여 화면에 표시
        /// </summary>
        public ObservableCollection<SnakeGameHistory> Scores { get; set; } = [];

        /// <summary>
        /// 현재 활성화된 뷰모델
        /// </summary>
        [ObservableProperty]
        private object? _currentViewModel;

        /// <summary>
        /// 성적데이터 표시 여부
        /// </summary>
        [ObservableProperty]
        private bool _isScoreVisible;

        /// <summary>
        /// 최종 점수
        /// </summary>
        [ObservableProperty]
        private int _finalScore;

        public SnakeGameEndViewModel(DashboardViewModel dashboardViewModel, int finalScore)
        {
            _dashboardViewModel = dashboardViewModel;
            FinalScore = finalScore;
        }

        /// <summary>
        /// 현재접속자의 상위 5개의 게임 데이터를 화면에 표시
        /// </summary>
        [RelayCommand]
        public async Task ShowTopScore()
        {
            Scores.Clear();

            string currentUser = _dashboardViewModel.CurrentUserId;

            try
            {
                List<SnakeGameHistory> topScores = await UserScores.LoadMyTopScoresAsync(currentUser);

                foreach (SnakeGameHistory history in topScores)
                {
                    Scores.Add(history);
                }

                IsScoreVisible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"데이터를 불러오는 중 오류가 발생했습니다: {ex.Message}");
            }
        }

        /// <summary>
        /// 게임화면으로 전환하여 게임 재시작.
        /// </summary>
        [RelayCommand]
        public void StartAgain()
        {
            CurrentViewModel = new SnakeGamePlayViewModel(_dashboardViewModel);
            ((SnakeGamePlayViewModel)CurrentViewModel).StartGame();
        }
    }
}
