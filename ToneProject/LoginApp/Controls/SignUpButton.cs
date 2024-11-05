using System.Windows;
using System.Windows.Controls;

namespace LoginApp.Controls
{
    public class SignUpButton : Button
    {
        static SignUpButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SignUpButton), new FrameworkPropertyMetadata(typeof(SignUpButton)));
        }
    }
}
