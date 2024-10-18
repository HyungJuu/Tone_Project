using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace LoginApp.ViewModels
{
    public partial class SignInSuccessViewModel(MainViewModel mainViewModel) : ObservableObject
    {
        private readonly MainViewModel _mainViewModel = mainViewModel;

        [RelayCommand]
        private void OnSignOut()
        {
            _mainViewModel.ShowSignInView();
        }
    }
}
