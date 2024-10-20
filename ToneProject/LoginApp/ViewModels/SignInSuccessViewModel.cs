using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace LoginApp.ViewModels
{
    /// <summary>
    /// 로그인성공 화면의 뷰모델.
    /// MainViewModel을 전달받아 로그아웃 시 로그인화면으로 전환하는 데 사용합니다.
    /// </summary>
    /// <param name="mainViewModel">MainViewModel 인스턴스</param>
    public partial class SignInSuccessViewModel(MainViewModel mainViewModel) : ObservableObject
    {
        /// <summary>
        /// MainViewModel를 참조하여 뷰 전환을 관리합니다.
        /// </summary>
        private readonly MainViewModel _mainViewModel = mainViewModel;

        /// <summary>
        /// 로그아웃을 처리하고 로그인 화면으로 돌아가는 명령을 실행합니다.
        /// </summary>
        [RelayCommand]
        private void OnSignOut()
        {
            _mainViewModel.ShowSignInView();
        }
    }
}
