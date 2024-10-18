using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace LoginApp.ViewModels
{
    public partial class SignInViewModel(MainViewModel mainViewModel) : ObservableObject
    {
        private readonly MainViewModel _mainViewModel = mainViewModel;

        [ObservableProperty]
        private string _id = "";

        [ObservableProperty]
        private string _password = "";

        [ObservableProperty]
        private string _loginStatus = "";

        [RelayCommand]
        private void SignIn()
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
        private static void SignUp()
        {
            var signUpView = new Views.SignUpView();
            signUpView.ShowDialog();
        }

        [RelayCommand]
        private static void FindId()
        {
            var FindIdView = new Views.FindIdView();
            FindIdView.ShowDialog();
        }

        [RelayCommand]
        private static void FindPassword()
        {
            var FindPasswordView = new Views.FindPasswordView();
            FindPasswordView.ShowDialog();
        }

        [RelayCommand]
        private static void SaveSignInInfo()
        {
        }
    }
}