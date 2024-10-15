using CommunityToolkit.Mvvm.ComponentModel;

namespace LoginApp.ViewModels
{
    public class FindPasswordViewModel : ObservableObject
    {
        private MainViewModel _mainViewModel;

        public FindPasswordViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;

        }
    }
}
