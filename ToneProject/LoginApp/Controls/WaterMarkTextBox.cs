using System.Windows;
using System.Windows.Controls;

namespace LoginApp.Controls
{
    public class WaterMarkTextBox : TextBox
    {
        static WaterMarkTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(WaterMarkTextBox), new FrameworkPropertyMetadata(typeof(WaterMarkTextBox)));
        }

        public static readonly DependencyProperty WatermarkProperty = DependencyProperty.Register(
        "Watermark", typeof(string), typeof(WaterMarkTextBox), new PropertyMetadata(default(string))); // 속성 이름, 속성 데이터타입, 속성이 정의된 클래스, 기본값==null

        public string Watermark
        {
            get => (string)GetValue(WatermarkProperty);
            set => SetValue(WatermarkProperty, value);
        }
    }
}
