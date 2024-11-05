using System.Windows;
using System.Windows.Controls;

namespace LoginApp.Controls
{
    public class SignInOutButton : Button
    {
        static SignInOutButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SignInOutButton), new FrameworkPropertyMetadata(typeof(SignInOutButton)));
        }
    }
}
