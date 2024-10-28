using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LoginApp.Models;

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
            if (ValidateSignUpInput())
            {
                _signUpViewModel.SignUpNext();
            }
        }

        /// <summary>
        /// 사용자 가입정보를 검증합니다.
        /// 유효하지 않은 경우 각 상태 메시지를 업데이트합니다.
        /// </summary>
        /// <returns>입력값의 유효여부를 나타내는 bool 값입니다.</returns>
        private bool ValidateSignUpInput()
        {
            // 모든 상태 메시지를 초기화
            SignUpIdStatus = string.Empty;
            SignUpPasswordStatus = string.Empty;
            SignUpConfirmPasswordStatus = string.Empty;

            bool isValid = true;

            if (string.IsNullOrEmpty(SignUpId))
            {
                SignUpIdStatus = "아이디를 입력해주세요";
                isValid = false;
            }
            else if (SignUpId.Length < 3)
            {
                SignUpIdStatus = "아이디를 3자리 이상 입력해주세요";
                isValid = false;
            }
            else if (DoesUserIdExist(SignUpId))
            {
                SignUpIdStatus = "이미 존재하는 아이디입니다.";
                isValid = false;
            }

            if (string.IsNullOrEmpty(SignUpPassword))
            {
                SignUpPasswordStatus = "비밀번호를 입력해주세요";
                isValid = false;
            }
            else if (SignUpPassword.Length < 4)
            {
                SignUpPasswordStatus = "비밀번호를 4자리 이상 입력해주세요";
                isValid = false;
            }

            if (!string.IsNullOrEmpty(SignUpPassword) && string.IsNullOrEmpty(SignUpConfirmPassword))
            {
                SignUpConfirmPasswordStatus = "비밀번호 확인이 필요합니다";
                isValid = false;
            }

            if (!string.IsNullOrEmpty(SignUpPassword) && !string.IsNullOrEmpty(SignUpConfirmPassword) && SignUpPassword != SignUpConfirmPassword)
            {
                SignUpConfirmPasswordStatus = "비밀번호가 일치하지 않습니다";
                isValid = false;
            }

            return isValid;
        }

        /// <summary>
        /// 데이터베이스에서 아이디가 존재하는지 확인합니다.
        /// </summary>
        /// <param name="userId">확인할 사용자 아이디</param>
        /// <returns>아이디가 존재하면 true, 존재하지 않으면 false 반환합니다.</returns>
        private static bool DoesUserIdExist(string userId)
        {
            using var context = new ToneProjectContext();
            return context.UserInfos.Any(u => u.Id == userId);
        }
    }
}
