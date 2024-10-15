using CommunityToolkit.Mvvm.ComponentModel;

namespace LoginApp.ViewModels
{
    public class FindAccountViewModel : ObservableObject
    {
        private MainViewModel _mainViewModel;
        public FindAccountViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;

        }
    }
}
