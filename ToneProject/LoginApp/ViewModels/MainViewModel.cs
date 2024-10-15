using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
