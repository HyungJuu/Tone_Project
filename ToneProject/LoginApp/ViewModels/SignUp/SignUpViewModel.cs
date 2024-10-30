using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LoginApp.DbContexts;
using System.Windows;

namespace LoginApp.ViewModels
{
    /// <summary>
    /// 회원가입 과정을 관리하는 뷰모델.
    /// 각 회원가입 단계의 화면 전환을 처리합니다.
    /// </summary>
    public partial class SignUpViewModel : ObservableObject
    {
        private readonly UserInfoContext _dbContext;

        /// <summary>
        /// 현재 회원가입 화면에 표시될 뷰모델을 나타냅니다
        /// </summary>
        [ObservableProperty]
        private object _currentSignUpViewModel;

        /// <summary>
        /// SignUpAccountInfoViewModel(1단계 회원가입 화면)을 기본 화면으로 설정합니다
        /// </summary>
        public SignUpViewModel()
        {
            _dbContext = new UserInfoContext();
            CurrentSignUpViewModel = new SignUpAccountInfoViewModel(this, _dbContext);
        }

        /// <summary>
        /// SignUpPersonalInfoViewModel(2단계 회원가입 화면)으로 전환합니다.
        /// 앞단계(SignUpAccountInfoViewModel)의 정보를 다음단계(SignUpPersonalInfoViewModel)로 가져갑니다.
        /// </summary>
        [RelayCommand]
        public void SignUpNext()
        {
            CurrentSignUpViewModel = new SignUpPersonalInfoViewModel((SignUpAccountInfoViewModel)CurrentSignUpViewModel);
        }

        /// <summary>
        /// 회원가입창을 닫고 로그인화면으로 돌아갑니다.
        /// </summary>
        /// <param name="currentWindow">현재 열려 있는 회원가입 창입니다.</param>
        [RelayCommand]
        public static void BackToSignIn(Window currentWindow)
        {
            currentWindow?.Close();
        }
    }
}
