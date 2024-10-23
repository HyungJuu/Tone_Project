using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LoginApp.ViewModels
{
    public partial class SignUpViewModel: ObservableObject
    {
        [ObservableProperty]
        private object _currentSignUpViewModel;

        public SignUpViewModel()
        {
            CurrentSignUpViewModel = new SignUpStep1ViewModel(this);
        }

        [RelayCommand]
        public void SignUpNext()
        {
            CurrentSignUpViewModel = new SignUpStep2ViewModel(this);
        }

        [RelayCommand]
        public void BackToSignIn(Window currentWindow)
        {
            currentWindow?.Close();
        }
    }
}
