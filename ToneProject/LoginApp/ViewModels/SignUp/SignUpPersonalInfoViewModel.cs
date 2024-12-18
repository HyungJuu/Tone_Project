﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LoginApp.DbContexts;
using LoginApp.Enums;
using LoginApp.Models;
using LoginApp.Validators;
using LoginApp.ViewModels.SignUp;
using System.Windows;

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

        private static readonly ToneProjectContext _dbContext = new();

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
        private Gender _signUpGender = Gender.None;

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

        [RelayCommand]
        public void SelectGender(object parameter)
        {
            if (parameter is Gender gender)
            {
                SignUpGender = gender;
            }
        }

        /// <summary>
        /// 사용자의 입력을 확인 후, 결과에 따라 입력 정보를 데이터베이스에 저장하여 회원가입을 완료합니다.
        /// </summary>
        /// <param name="currentWindow">현재 활성화된 창</param>
        [RelayCommand]
        public void SignUpEnd(Window currentWindow)
        {
            SignUpPersonalResult result = SignUpPersonalValidator.CheckSignUp(SignUpName, SignUpBirth);

            if (!result.IsValid)
            {
                SignUpNameStatus = result.NameStatus;
                SignUpBirthStatus = result.BirthStatus;

                return;
            }
            SaveUserInfoToDatabase();
            SignUpViewModel.BackToSignIn(currentWindow);
        }

        /// <summary>
        /// 사용자 정보를 데이터베이스에 저장합니다.
        /// </summary>
        private void SaveUserInfoToDatabase()
        {
            string gender = SignUpGender switch
            {
                Gender.Male => "남",
                Gender.Female => "여",
                Gender.None => "선택안함",
                _ => "선택안함"
            };

            UserInfo userInfo = new()
            {
                UserId = _signUpAccountInfoViewModel.SignUpId,
                Pwd = _signUpAccountInfoViewModel.SignUpPassword,
                Name = SignUpName,
                Birth = DateOnly.ParseExact(SignUpBirth, "yyyyMMdd", null),
                Gender = gender
            };

            _dbContext.UserInfos.Add(userInfo);
            _dbContext.SaveChanges();
        }
    }
}