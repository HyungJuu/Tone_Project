using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginApp.ViewModels
{
    public partial class SignUpStep1ViewModel(SignUpViewModel signUpViewModel) : ObservableObject
    {
        private readonly SignUpViewModel _signUpViewModel = signUpViewModel;

        [ObservableProperty]
        private string _signUpId = "";

        [ObservableProperty]
        private string _signUpPassword = "";

        [ObservableProperty]
        private string _signUpConfirmPassword = "";

        [ObservableProperty]
        private string _signUpIdStatus = "";

        [ObservableProperty]
        private string _signUpPasswordStatus = "";

        [ObservableProperty]
        private string _signUpConfirmPasswordStatus = "";


        [RelayCommand]
        public void SignUpNext()
        {
            if (ValidateSignUpInput())
            {
                _signUpViewModel.SignUpNext();
            }
        }

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

            if (string.IsNullOrEmpty(SignUpPassword))
            {
                SignUpPasswordStatus = "비밀번호를 입력해주세요";
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

    }
}
