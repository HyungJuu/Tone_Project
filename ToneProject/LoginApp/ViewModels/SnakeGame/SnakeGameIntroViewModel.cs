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
            SnakeGamePlayViewModel snakeGamePlayViewModel = new();
            snakeGamePlayViewModel.StartGame();
            CurrentViewModel = snakeGamePlayViewModel; // CurrentViewModel을 SnakeGamePlayViewModel로 설정하여 화면 전환
        }
    }
}
