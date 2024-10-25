using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LoginApp.Models;
using System;
using System.Windows;

namespace LoginApp.ViewModels
{
    public partial class SignUpStep2ViewModel(SignUpStep1ViewModel signUpStep1ViewModel) : ObservableObject
    {
        private readonly SignUpStep1ViewModel _signUpStep1ViewModel = signUpStep1ViewModel;

        [ObservableProperty]
        private string _signUpName = "";

        [ObservableProperty]
        private string _signUpBirth = "";

        [ObservableProperty]
        private string _signUpGender = "";

        [ObservableProperty]
        private string _signUpNameStatus = string.Empty;

        [ObservableProperty]
        private string _signUpBirthStatus = string.Empty;

        [ObservableProperty]
        private string _signUpGenderStatus = string.Empty;

        [RelayCommand]
        public void SignUpEnd(Window currentWindow)
        {
            if (ValidateSignUpInput())
            {
                SaveUserInfoToDatabase();
                SignUpViewModel.BackToSignIn(currentWindow);
            }
        }

        [RelayCommand]
        private void SetGender(string gender)
        {
            SignUpGender = gender;
        }


        private void SaveUserInfoToDatabase()
        {
            using var context = new ToneProjectContext();

            // 생년월일 문자열을 DateTime으로 변환
            DateTime birthDate = DateTime.ParseExact(SignUpBirth, "yyyyMMdd", null);

            var userInfo = new UserInfo
            {
                Id = _signUpStep1ViewModel.SignUpId,
                Pwd = _signUpStep1ViewModel.SignUpPassword,
                Name = SignUpName,
                Birth = DateOnly.FromDateTime(birthDate), // DateTime을 DateOnly로 변환
                Gender = SignUpGender
            };

            context.UserInfos.Add(userInfo);
            context.SaveChanges();
        }

        private bool ValidateSignUpInput()
        {
            // 모든 상태 메시지 초기화
            SignUpNameStatus = string.Empty;
            SignUpBirthStatus = string.Empty;
            SignUpGenderStatus = string.Empty;

            bool isValid = true;

            if (string.IsNullOrEmpty(SignUpName))
            {
                SignUpNameStatus = "이름을 입력해 주세요";
                isValid = false;
            }

            if (string.IsNullOrEmpty(SignUpBirth))
            {
                SignUpBirthStatus = "생년월일을 입력해 주세요";
                isValid = false;
            }
            else if (SignUpBirth.Length != 8 || !DateTime.TryParseExact(SignUpBirth, "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out _))
            {
                SignUpBirthStatus = "생년월일은 8자리 숫자로 입력해 주세요";
                isValid = false;
            }

            if (string.IsNullOrEmpty(SignUpGender))
            {
                SignUpGenderStatus = "성별을 선택해 주세요";
                isValid = false;
            }

            return isValid;
        }
    }
}