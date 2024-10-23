﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Windows;

namespace LoginApp.ViewModels
{
    public partial class SignUpStep2ViewModel : ObservableObject
    {
        private readonly SignUpViewModel _signUpViewModel;

        public SignUpStep2ViewModel(SignUpViewModel signUpViewModel)
        {
            _signUpViewModel = signUpViewModel;
        }

        [RelayCommand]
        public void SignUpEnd(Window currentWindow)
        {
            _signUpViewModel.BackToSignIn(currentWindow);
        }
    }
}