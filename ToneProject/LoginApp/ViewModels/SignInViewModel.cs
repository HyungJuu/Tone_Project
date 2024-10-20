using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace LoginApp.ViewModels
{
    /// <summary>
    /// 로그인 화면의 뷰모델.
    /// 로그인, 회원가입, 아이디/비밀번호 찾기 등의 로직을 처리합니다.
    /// </summary>
    /// <param name="mainViewModel"></param>
    public partial class SignInViewModel(MainViewModel mainViewModel) : ObservableObject
    {
        /// <summary>
        /// MainViewModel을 참조하여 로그인 성공 시 화면 전환을 처리합니다.
        /// </summary>
        private readonly MainViewModel _mainViewModel = mainViewModel;

        /// <summary>
        /// 로그인할 때 사용자의 아이디를 저장하는 속성입니다.
        /// </summary>
        [ObservableProperty]
        private string _id = "";

        /// <summary>
        /// 로그인할 때 사용자의 비밀번호를 저장하는 속성입니다.
        /// </summary>
        [ObservableProperty]
        private string _password = "";

        /// <summary>
        /// 로그인 상태(성공, 실패 등) 메시지를 저장하는 속성입니다.
        /// </summary>
        [ObservableProperty]
        private string _loginStatus = "";

        /// <summary>
        /// 비밀번호 표시 여부를 관리하는 속성입니다.
        /// </summary>
        [ObservableProperty]
        private bool _isPasswordVisible = false;

        /// <summary>
        /// 사용자가 로그인버튼을 눌렀을 때 실행되는 명령입니다.
        /// 아이디/비밀번호의 일치여부를 통해 로그인 상태메시지를 표시하거나 로그인성공 화면으로 전환합니다.
        /// </summary>
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
                _mainViewModel.ShowSignInSuccessView(); // 로그인성공 화면 전환
            }
            else if (Id == userId)
            {
                LoginStatus = "비밀번호가 올바르지 않습니다.";
                Password = string.Empty;
            }
            else
            {
                LoginStatus = "아이디 또는 비밀번호가 올바르지 않습니다";
                Id = string.Empty;
                Password = string.Empty;
            }
        }

        /// <summary>
        /// 회원가입 화면을 보여주는 명령입니다.
        /// </summary>
        [RelayCommand]
        private static void SignUp()
        {
            var signUpView = new Views.SignUpView();
            signUpView.ShowDialog();
        }

        /// <summary>
        /// 아이디 찾기 화면을 보여주는 명령입니다.
        /// </summary>
        [RelayCommand]
        private static void FindId()
        {
            var FindIdView = new Views.FindIdView();
            FindIdView.ShowDialog();
        }

        /// <summary>
        /// 비밀번호 찾기 화면을 보여주는 명령입니다.
        /// </summary>
        [RelayCommand]
        private static void FindPassword()
        {
            var FindPasswordView = new Views.FindPasswordView();
            FindPasswordView.ShowDialog();
        }

        /// <summary>
        /// 비밀번호 표시 여부를 전환(표시/숨김)하는 명령입니다.
        /// </summary>
        [RelayCommand]
        private void ShowPassword()
        {
            IsPasswordVisible = !IsPasswordVisible; // 현재 비밀번호 표시상태의 반대값으로 변경
        }
    }
}