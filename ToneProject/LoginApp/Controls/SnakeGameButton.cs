using System.Windows;
using System.Windows.Controls;

namespace LoginApp.Controls
{
    public class SnakeGameButton : Button
    {
        static SnakeGameButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SnakeGameButton), new FrameworkPropertyMetadata(typeof(SnakeGameButton)));
        }
    }
}
