using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LoginApp.DbContexts;
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

            string? loggedInUserId = _signInSuccessViewModel.CurrentUserId; // 로그인된 사용자 아이디

            try
            {
                using (var context = new SnakeGameContext())
                {
                    var topScores = context.SnakeGameRecords
                        .Where(r => r.UserId == loggedInUserId)  // 로그인 아이디의 데이터 조회
                        .OrderByDescending(r => r.GameClear)  // 게임 클리어 기준으로 내림차순 정렬
                        .ThenByDescending(r => r.Score)  // 점수를 기준으로 내림차순 정렬
                        .Take(5)  // 상위 5개 레코드만 가져옴
                        .ToList();

                    // 가져온 데이터를 Scores ObservableCollection에 추가
                    foreach (var record in topScores)
                    {
                        Scores.Add(record);
                    }
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
            SnakeGamePlayViewModel snakeGamePlayViewModel = new();
            snakeGamePlayViewModel.StartGame();
            CurrentViewModel = snakeGamePlayViewModel;
        }
    }
}
