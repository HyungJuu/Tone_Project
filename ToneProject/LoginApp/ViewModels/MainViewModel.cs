using CommunityToolkit.Mvvm.ComponentModel;
using LoginApp.Views;

namespace LoginApp.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        private object _currentViewModel;

        public MainViewModel()
        {
            CurrentViewModel = new SignInViewModel(this);
        }

        // 상황에 따라 각 메서드를 호출하여 CurrentViewModel 속성을 변경 -> 화면전환
        public void ShowMainView()
        {
            CurrentViewModel = new SignInSuccessViewModel(this);
        }
        /// <summary>
        /// 
        /// </summary>
        public void ShowSignInView()
        {
            CurrentViewModel = new SignInViewModel(this);
        }

        public void ShowSignUpView()
        {
            var signUpView = new Views.SignUpView();
            signUpView.Show();
        }

        public void ShowFindAccountView()
        {
            var FindAccountView = new Views.FindAccountView();
            FindAccountView.Show();
        }

        public void ShowFindPasswordView()
        {
            var FindPasswordView = new Views.FindPasswordView();
            FindPasswordView.Show();
        }
    }
}
