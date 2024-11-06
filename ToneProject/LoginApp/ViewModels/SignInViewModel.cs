using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LoginApp.Utils;
using LoginApp.Views;
using static LoginApp.Utils.ValidationHelper;


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
            // 로그인 후 과정 확인용
            if (Id == "admin" && Password == "admin")
            {
                _mainViewModel.ShowSignInSuccessView(Id);
            }

            SignInResult result = ValidationHelper.CheckSignIn(Id, Password);

            if (!result.IsValid)
            {
                LoginStatus = result.Message;
                Id = result.ClearId ? string.Empty : Id;
                Password = result.ClearPassword ? string.Empty : Password;
                return;
            }

            _mainViewModel.ShowSignInSuccessView(Id);
        }

        /// <summary>
        /// 회원가입 창을 보여줍니다.
        /// </summary>
        [RelayCommand]
        private static void SignUp()
        {
            SignUpView signUpView = new()
            {
                DataContext = new SignUpViewModel()
            };
            signUpView.ShowDialog();
        }

        /// <summary>
        /// 아이디 찾기 창을 보여줍니다.
        /// </summary>
        [RelayCommand]
        private static void FindId()
        {
            FindIdView FindIdView = new();
            FindIdView.ShowDialog();
        }

        /// <summary>
        /// 비밀번호 찾기 창을 보여줍니다.
        /// </summary>
        [RelayCommand]
        private static void FindPassword()
        {
            FindPasswordView FindPasswordView = new();
            FindPasswordView.ShowDialog();
        }
    }
}