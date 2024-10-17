using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace LoginApp.ViewModels
{
    public partial class SignInViewModel : ObservableObject
    {
        private MainViewModel _mainViewModel;

        [ObservableProperty]
        private string _id = "";

        [ObservableProperty]
        private string _password = "";

        [ObservableProperty]
        private string _loginStatus = "";

        public SignInViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
        }

        [RelayCommand]
        private void OnSignIn()
        {
            // 임시 아이디, 비밀번호
            string userId = "admin";
            string userPassword = "1234";

            if (string.IsNullOrEmpty(Id))
            {
                LoginStatus = "아이디를 입력하세요";
            }
            else if (string.IsNullOrEmpty(Password))
            {
                LoginStatus = "비밀번호를 입력하세요";
            }
            else if (Id == userId && Password == userPassword)
            {
                _mainViewModel.ShowSignInSuccessView();
            }
            else if (Id == userId)
            {
                LoginStatus = "비밀번호가 올바르지 않습니다.";
            }
            else
            {
                LoginStatus = "아이디 또는 비밀번호가 올바르지 않습니다";
            }
        }

        [RelayCommand]
        private void OnSignUp()
        {
            var signUpView = new Views.SignUpView();
            signUpView.ShowDialog();
        }

        [RelayCommand]
        private void OnFindId()
        {
            var FindIdView = new Views.FindIdView();
            FindIdView.ShowDialog();
        }

        /// <summary>
        /// 
        /// </summary>
        [RelayCommand]
        private void OnFindPassword()
        {
            var FindPasswordView = new Views.FindPasswordView();
            FindPasswordView.ShowDialog();
        }
    }
}