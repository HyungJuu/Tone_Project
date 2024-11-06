using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LoginApp.ViewModels.SnakeGame;

namespace LoginApp.ViewModels
{
    /// <summary>
    /// 로그인성공 화면의 뷰모델.
    /// MainViewModel을 전달받아 로그아웃 시 로그인화면으로 전환하는 데 사용합니다.
    /// </summary>
    public partial class SignInSuccessViewModel : ObservableObject
    {
        /// <summary>
        /// MainViewModel를 참조하여 뷰 전환을 관리합니다.
        /// </summary>
        private readonly MainViewModel _mainViewModel;

        [ObservableProperty]
        private object? _currentGameViewModel;


        /// <summary>
        /// 로그인한 사용자의 ID를 저장
        /// </summary>
        [ObservableProperty]
        private string? _currentUserId;

        
        public SignInSuccessViewModel(MainViewModel mainViewModel, string currentUserId)
        {
            _mainViewModel = mainViewModel;
            CurrentUserId = currentUserId; // ID 저장
        }

        /// <summary>
        /// 로그아웃을 처리하고 로그인 화면으로 돌아가는 명령을 실행합니다.
        /// </summary>
        [RelayCommand]
        private void SignOut()
        {
            _mainViewModel.ShowSignInView();
            CurrentUserId = null; // 로그아웃 시 ID 초기화
        }

        [RelayCommand]
        private void SelectSnakeGame()
        {
            CurrentGameViewModel = new SnakeGameIntroViewModel(this);
        }

        [RelayCommand]
        public void BackToSelectGame()
        {
            CurrentGameViewModel = null;
        }
    }
}
