using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LoginApp.DbContexts;
using LoginApp.Models;
using LoginApp.Utils;
using System.Windows.Controls;


namespace LoginApp.ViewModels
{
    /// <summary>
    /// 로그인 화면의 뷰모델.
    /// 로그인, 회원가입, 아이디/비밀번호 찾기 등의 로직을 처리합니다.
    /// </summary>
    /// <param name="mainViewModel"></param>
    public partial class SignInViewModel(MainViewModel mainViewModel, UserInfoContext dbContext) : ObservableObject
    {
        /// <summary>
        /// MainViewModel을 참조하여 로그인 성공 시 화면 전환을 처리합니다.
        /// </summary>
        private readonly MainViewModel _mainViewModel = mainViewModel;

        /// <summary>
        /// UserInfoContext를 참조하여 데이터베이스와 상호작용합니다. 
        /// </summary>
        private readonly UserInfoContext _dbContext = dbContext;

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
        public void SignIn()
        {
            var validationMessage = ValidationHelper.ValidateSignInCredentials(Id, Password, _dbContext);

            if (!string.IsNullOrEmpty(validationMessage))
            {
                LoginStatus = validationMessage;
                if (validationMessage.Contains("아이디")) Id = string.Empty;
                if (validationMessage.Contains("비밀번호")) Password = string.Empty;
            }
            else
            {
                // 로그인 성공 시 호출
                _mainViewModel.ShowSignInSuccessView();
            }
        }

        /// <summary>
        /// 회원가입 창을 보여줍니다.
        /// </summary>
        [RelayCommand]
        private static void SignUp()
        {
            var signUpView = new Views.SignUpView
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
            var FindIdView = new Views.FindIdView();
            FindIdView.ShowDialog();
        }

        /// <summary>
        /// 비밀번호 찾기 창을 보여줍니다.
        /// </summary>
        [RelayCommand]
        private static void FindPassword()
        {
            var FindPasswordView = new Views.FindPasswordView();
            FindPasswordView.ShowDialog();
        }
    }
}