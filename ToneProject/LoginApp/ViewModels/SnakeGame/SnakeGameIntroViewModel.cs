using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace LoginApp.ViewModels.SnakeGame
{
    public partial class SnakeGameIntroViewModel(SignInSuccessViewModel signInSuccessViewModel) : ObservableObject
    {
        private readonly SignInSuccessViewModel _signInSuccessViewModel = signInSuccessViewModel;
        [ObservableProperty]
        private object? _currentViewModel;

        [RelayCommand]
        public void SelectGame()
        {
            _signInSuccessViewModel.GoToSelectGame();
        }

        [RelayCommand]
        public void StartSnakeGame()
        {
            // SnakeGamePlayViewModel을 생성하고 타이머 시작 메서드 호출
            var snakeGamePlayViewModel = new SnakeGamePlayViewModel();
            snakeGamePlayViewModel.StartGame(); // 게임 시작 (3초 카운트다운 포함)
            CurrentViewModel = snakeGamePlayViewModel; // CurrentViewModel을 SnakeGamePlayViewModel로 설정하여 화면 전환
        }
    }
}
