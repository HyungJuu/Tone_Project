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
        private SignUpViewModel _signUpViewModel = signUpViewModel;

        [RelayCommand]
        public void SignUpNext()
        {
            _signUpViewModel.SignUpNext();
        }
    }
}
