﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LoginApp.Models;
using System.Collections.ObjectModel;
using System.Windows;

namespace LoginApp.ViewModels.SnakeGame
{
    /// <summary>
    /// 게임 종료 후 성적조회 및 재시작 기능을 제공하는 뷰모델 클래스
    /// </summary>
    /// <param name="dashboardViewModel"></param>
    public partial class SnakeGameEndViewModel(DashboardViewModel dashboardViewModel) : ObservableObject
    {
        private readonly DashboardViewModel _dashboardViewModel = dashboardViewModel;

        /// <summary>
        /// 사용자의 게임 기록을 저장
        /// </summary>
        public ObservableCollection<SnakeGameRecord> Scores { get; set; } = [];

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

        /// <summary>
        /// 게임화면으로 전환하여 게임 재시작.
        /// </summary>
        [RelayCommand]
        public void StartAgain()
        {
            SnakeGamePlayViewModel snakeGamePlayViewModel = new(_dashboardViewModel);
            snakeGamePlayViewModel.StartGame();
            CurrentViewModel = snakeGamePlayViewModel;
        }
    }
}