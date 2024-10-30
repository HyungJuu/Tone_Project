using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LoginApp.DbContexts;
using LoginApp.Models;
using System.Windows;
using static LoginApp.ViewModels.SignUpPersonalInfoViewModel;

namespace LoginApp.ViewModels
{
    /// <summary>
    /// 2단계 회원가입 화면 뷰모델.
    /// 사용자가 이름, 생년월일, 성별을 입력하는 화면을 처리합니다.
    /// </summary>
    /// <param name="signUpAccountInfoViewModel">1단계 회원가입 뷰모델입니다.</param>
    public partial class SignUpPersonalInfoViewModel(SignUpAccountInfoViewModel signUpAccountInfoViewModel) : ObservableObject
    {
        /// <summary>
        /// SignUpAccountInfoViewModel(1단계회원가입)의 인스턴스를 참조하여 회원가입 과정에서 사용합니다.
        /// </summary>
        private readonly SignUpAccountInfoViewModel _signUpAccountInfoViewModel = signUpAccountInfoViewModel;

        /// <summary>
        /// 사용자가 입력한 이름을 저장합니다.
        /// </summary>
        [ObservableProperty]
        private string _signUpName = "";

        /// <summary>
        /// 사용자가 입력한 생년월일을 저장합니다.
        /// </summary>
        [ObservableProperty]
        private string _signUpBirth = "";

        /// <summary>
        /// 사용자가 선택한 성별을 저장합니다.
        /// </summary>
        [ObservableProperty]
        private Gender _signUpGender = Gender.선택안함;

        /// <summary>
        /// 이름 입력 상태 메시지를 저장합니다.
        /// </summary>
        [ObservableProperty]
        private string _signUpNameStatus = string.Empty;

        /// <summary>
        /// 생년월일 입력 상태 메시지를 저장합니다.
        /// </summary>
        [ObservableProperty]
        private string _signUpBirthStatus = string.Empty;

        /// <summary>
        /// 성별 입력 상태 메시지를 저장합니다.
        /// </summary>
        [ObservableProperty]
        private string _signUpGenderStatus = string.Empty;

        public enum Gender
        {
            남성,
            여성,
            선택안함
        }

        /// <summary>
        /// 사용자의 입력을 검증하고, 유효한 경우 사용자 정보를 데이터베이스에 저장하여 회원가입을 완료합니다.
        /// </summary>
        /// <param name="currentWindow">현재 활성화된 창입니다.</param>
        [RelayCommand]
        public void SignUpEnd(Window currentWindow)
        {
            if (ValidateSignUpInput())
            {
                SaveUserInfoToDatabase();
                SignUpViewModel.BackToSignIn(currentWindow);
            }
        }

        /// <summary>
        /// 사용자 정보를 데이터베이스에 저장합니다.
        /// </summary>
        private void SaveUserInfoToDatabase()
        {
            using var context = new UserInfoContext();

            var userInfo = new UserInfo
            {
                Id = _signUpAccountInfoViewModel.SignUpId,
                Pwd = _signUpAccountInfoViewModel.SignUpPassword,
                Name = SignUpName,
                Birth = DateOnly.ParseExact(SignUpBirth, "yyyyMMdd", null),
                Gender = SignUpGender.ToString() // Enum 값을 문자열로 저장
            };

            context.UserInfos.Add(userInfo);
            context.SaveChanges();
        }

        /// <summary>
        /// 사용자 가입정보를 검증합니다. 
        /// 유효하지 않은 경우 상태 메시지를 업데이트합니다.
        /// </summary>
        /// <returns>모든 입력값이 유효하면 true, 그렇지 않으면 false를 반환합니다.</returns>
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
            else if (SignUpBirth.Length != 8 || !DateOnly.TryParseExact(SignUpBirth, "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out _))
            {
                SignUpBirthStatus = "생년월일 형식에 맞는\n8자리 숫자로 입력해 주세요";
                isValid = false;
            }

            return isValid;
        }

        
    }
}