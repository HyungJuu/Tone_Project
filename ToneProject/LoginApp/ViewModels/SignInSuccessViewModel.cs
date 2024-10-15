using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace LoginApp.ViewModels
{
    public partial class SignInSuccessViewModel : ObservableObject
    {
        private MainViewModel _mainViewModel;

        public SignInSuccessViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
        }

        /// <summary>
        /// 로그아웃 버튼을 통해 로그인화면으로 전환
        /// </summary>
        [RelayCommand]
        private void OnSignOut()
        {
            _mainViewModel.ShowSignInView();
        }
    }
}
