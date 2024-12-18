﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LoginApp.Models;
using System.Collections.ObjectModel;
using System.Windows;

namespace LoginApp.ViewModels.SnakeGame
{
    /// <summary>
    /// 게임 선택 후 성적조회 및 시작 기능을 제공하는 뷰모델 클래스
    /// </summary>
    /// <param name="dashboardViewModel"></param>
    public partial class SnakeGameIntroViewModel(DashboardViewModel dashboardViewModel) : ObservableObject
    {
        private readonly DashboardViewModel _dashboardViewModel = dashboardViewModel;

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
        /// 현재접속자의 상위 5개의 게임 데이터를 화면에 표시
        /// </summary>
        [RelayCommand]
        public async Task ShowMyTopScoreAsync()
        {
            Scores.Clear();

            string currentUser = _dashboardViewModel.CurrentUserId;

            try
            {
                List<SnakeGameHistory> topScores;

                if (currentUser == "admin")
                {
                    topScores = AdminScores.GetAdminScores();
                }
                else
                {
                    topScores = await UserScores.LoadMyTopScoresAsync(currentUser);
                }

                if (topScores == null || topScores.Count == 0)
                {
                    MessageBox.Show("조회할 성적이 없습니다.");
                    return;
                }

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
        /// 스네이크 게임 전체 데이터 중 상위 5개를 화면에 표시하는 비동기 메서드
        /// </summary>
        /// <returns></returns>
        [RelayCommand]
        public async Task ShowTotalTopScoreAsync()
        {
            Scores.Clear();
            string currentUser = _dashboardViewModel.CurrentUserId;

            try
            {
                List<SnakeGameHistory> topScores;
                if (currentUser == "admin")
                {
                    topScores = AdminScores.GetTotalScores();
                }
                else
                {
                    topScores = await UserScores.LoadTotalTopScoreAsync();
                }

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
        /// 게임선택 화면으로 돌아가기
        /// </summary>
        [RelayCommand]
        public void BackToMain()
        {
            _dashboardViewModel.BackToSelectGame();
        }

        /// <summary>
        /// 게임화면으로 전환하여 게임 시작.
        /// </summary>
        [RelayCommand]
        public void StartSnakeGame()
        {
            CurrentViewModel = new SnakeGamePlayViewModel(_dashboardViewModel);
            ((SnakeGamePlayViewModel)CurrentViewModel).StartGame();
        }
    }
}