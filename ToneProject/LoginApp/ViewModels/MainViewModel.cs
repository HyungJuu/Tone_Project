using CommunityToolkit.Mvvm.ComponentModel;
using LoginApp.Views;

namespace LoginApp.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        private object _currentViewModel;

        public MainViewModel()
        {
            CurrentViewModel = new SignInViewModel(this);
        }

        public void ShowSignInSuccessView()
        {
            CurrentViewModel = new SignInSuccessViewModel(this);
        }
        /// <summary>
        /// 
        /// </summary>
        public void ShowSignInView()
        {
            CurrentViewModel = new SignInViewModel(this);
        }
    }
}
