using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace LoginApp.ViewModels.SnakeGame
{
    public partial class SnakeGameIntroViewModel : ObservableObject
    {
        private readonly SignInSuccessViewModel _signInSuccessViewModel;

        public SnakeGameIntroViewModel(SignInSuccessViewModel signInSuccessViewModel)
        {
            _signInSuccessViewModel = signInSuccessViewModel;
        }

        [RelayCommand]
        public void SelectGame()
        {
            _signInSuccessViewModel.GoToSelectGame();
        }
    }
}
