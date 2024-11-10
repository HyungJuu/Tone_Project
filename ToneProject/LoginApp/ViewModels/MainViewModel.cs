using CommunityToolkit.Mvvm.ComponentModel;

namespace LoginApp.ViewModels
{
    /// <summary>
    /// 로그인 화면과 로그인성공 화면 간 뷰 전환을 관리하는 뷰모델입니다
    /// </summary>
    public partial class MainViewModel : ObservableObject
    {

        /// <summary>
        /// 현재 화면에 표시될 뷰모델을 나타냅니다
        /// </summary>
        [ObservableProperty]
        private object _currentViewModel;

        /// <summary>
        /// MainViewModel의 생성자.
        /// 초기화 시 SignInViewModel(로그인화면)을 기본 화면으로 설정합니다
        /// </summary>
        public MainViewModel()
        {
            CurrentViewModel = new SignInViewModel(this);
        }

        /// <summary>
        /// 로그인 성공 시 호출되는 메서드.
        /// DashboardViewModel(로그인성공 화면)으로 전환합니다.
        /// </summary>
        public void ShowDashboardView(string userId)
        {
            CurrentViewModel = new DashboardViewModel(this, userId);
        }

        /// <summary>
        /// 로그인 화면으로 돌아갈 때 호출되는 메서드.
        /// SignInViewModel(로그인 화면)으로 전환합니다.
        /// </summary>
        public void ShowSignInView()
        {
            CurrentViewModel = new SignInViewModel(this);
        }
    }
}
