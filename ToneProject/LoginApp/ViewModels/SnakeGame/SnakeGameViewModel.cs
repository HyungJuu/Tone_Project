using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginApp.ViewModels.SnakeGame
{
    public class SnakeGameViewModel
    {
        private SignInSuccessViewModel signInSuccessViewModel;

        public SnakeGameViewModel(SignInSuccessViewModel signInSuccessViewModel)
        {
            this.signInSuccessViewModel = signInSuccessViewModel;
        }
    }
}
