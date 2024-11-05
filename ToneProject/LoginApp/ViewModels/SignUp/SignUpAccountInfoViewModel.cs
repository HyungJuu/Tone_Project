using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LoginApp.DbContexts;
using LoginApp.Utils;
using System.Net.NetworkInformation;

namespace LoginApp.ViewModels
{
    /// <summary>
    /// 1단계 회원가입 화면 뷰모델
    /// 사용자가 아이디와 비밀번호를 입력하는 화면을 처리합니다.
    /// </summary>
    /// <param name="signUpViewModel">회원가입 과정에서 사용되는 상위 뷰모델입니다.</param>
    public partial class SignUpAccountInfoViewModel(SignUpViewModel signUpViewModel) : ObservableObject
    {
        /// <summary>
        /// SignUpViewModel의 인스턴스를 참조하여 회원가입 과정에서 사용합니다.
        /// </summary>
        private readonly SignUpViewModel _signUpViewModel = signUpViewModel;

        /// <summary>
        /// 사용자가 입력한 아이디를 저장합니다.
        /// </summary>
        [ObservableProperty]
        private string _signUpId = "";

        /// <summary>
        /// 사용자가 입력한 비밀번호를 저장합니다.
        /// </summary>
        [ObservableProperty]
        private string _signUpPassword = "";

        /// <summary>
        /// 사용자가 입력한 비밀번호 확인을 저장합니다.
        /// </summary>
        [ObservableProperty]
        private string _signUpConfirmPassword = "";

        /// <summary>
        /// 아이디 입력 상태 메시지를 저장합니다.
        /// </summary>
        [ObservableProperty]
        private string _signUpIdStatus = "";

        /// <summary>
        /// 비밀번호 입력 상태 메시지를 저장합니다.
        /// </summary>
        [ObservableProperty]
        private string _signUpPasswordStatus = "";

        /// <summary>
        /// 비밀번호 확인 상태 메시지를 저장합니다.
        /// </summary>
        [ObservableProperty]
        private string _signUpConfirmPasswordStatus = "";

        /// <summary>
        /// 사용자의 입력을 검증하고, 유효한 경우 다음 단계로 넘어갑니다.
        /// </summary>
        [RelayCommand]
        public void SignUpNext()
        {
            var result = ValidationHelper.CheckSignUp(SignUpId, SignUpPassword, SignUpConfirmPassword);

            if (!result.IsValid)
            {
                SignUpIdStatus = result.IdStatus;
                SignUpPasswordStatus = result.PwdStatus;
                SignUpConfirmPasswordStatus = result.PwdCheckStatus;

                return;
            }
            
            _signUpViewModel.SignUpNext();
        }
    }
}