using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace LoginApp.ViewModels
{
    public partial class SignInSuccessViewModel : ObservableObject
    {
        private MainViewModel _mainViewModel;

        public string SuccessMessage { get; } = "로그인에 성공했습니다!";


        public SignInSuccessViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
        }


        /// <summary>
        /// 
        /// </summary>
        [RelayCommand]
        private void OnSignOut()
        {
            _mainViewModel.ShowSignInView();
        }
    }
}
