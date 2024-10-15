using CommunityToolkit.Mvvm.ComponentModel;

namespace LoginApp.ViewModels
{
    public class SignUpViewModel : ObservableObject
    {
        private MainViewModel _mainViewModel;

        public SignUpViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;

        }
    }
}
