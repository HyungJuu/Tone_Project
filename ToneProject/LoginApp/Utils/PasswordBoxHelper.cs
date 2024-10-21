using System.Windows;
using System.Windows.Controls;

namespace LoginApp.Utils
{
    /// <summary>
    /// 보안상의 이유로 데이터바인딩이 불가능한 PasswordBox에 비밀번호를 바인딩할 수 있도록 도와주는 클래스.
    /// </summary>
    public class PasswordBoxHelper
    {
        /// <summary>
        /// PasswordBox에 바인딩된 비밀번호를 가져옵니다
        /// </summary>
        /// <param name="obj">비밀번호를 가져올 대상 PasswordBox입니다</param>
        /// <returns>현재 설정된 비밀번호입니다</returns>
        public static string GetBoundPassword(DependencyObject obj)
        {
            return (string)obj.GetValue(BoundPasswordProperty);
        }

        /// <summary>
        /// PasswordBox에 새로운 비밀번호를 설정합니다
        /// </summary>
        /// <param name="obj">비밀번호를 설정할 대상 PasswordBox입니다</param>
        /// <param name="value">새로 설정할 비밀번호입니다</param>
        public static void SetBoundPassword(DependencyObject obj, string value)
        {
            obj.SetValue(BoundPasswordProperty, value);
        }

        /// <summary>
        /// PasswordBox에 비밀번호를 바인딩하는 역할.
        /// PasswordBox의 비밀번호를 뷰모델과 자동으로 동기화할 수 있습니다.
        /// </summary>
        public static readonly DependencyProperty BoundPasswordProperty =
            DependencyProperty.RegisterAttached(
                "BoundPassword", // DependencyProperty에 등록할 속성 이름
                typeof(string), // 속성 타입
                typeof(PasswordBoxHelper), // DependencyProperty 사용 클래스
                new PropertyMetadata("<Default>", OnBoundPasswordChanged)); // 속성의 기본값, 콜백메서드

        /// <summary>
        /// PasswordBox의 비밀번호가 바뀌었을 때 호출되는 메서드.
        /// 변경된 비밀번호를 PasswordBox에 반영하고, 이벤트를 업데이트합니다
        /// </summary>
        /// <param name="d">비밀번호가 변경된 PasswordBox입니다</param>
        /// <param name="e">변경된 내용에 대한 정보입니다</param>
        private static void OnBoundPasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not PasswordBox passwordBox) return;

            // 이벤트를 제거하여 메모리 초기화
            passwordBox.PasswordChanged -= PasswordChanged;

            // 새로 설정된 비밀번호가 기존과 다르면 업데이트
            string newString = (string)e.NewValue ?? string.Empty;
            if (newString != passwordBox.Password)
            {
                passwordBox.Password = newString;
            }
            // 이벤트를 다시 생성
            passwordBox.PasswordChanged += PasswordChanged;
        }

        /// <summary>
        /// PasswordBox에서 비밀번호가 바뀌었을 때 실행되는 메서드.
        /// 비밀번호가 바뀌면 BoundPassword 속성을 업데이트해서 뷰모델과 동기화합니다
        /// </summary>
        /// <param name="sender">이벤트가 발생한 객체, PasswordBox입니다</param>
        /// <param name="e">이벤트에 대한 추가 정보입니다</param>
        private static void PasswordChanged(object sender, RoutedEventArgs e)
        {
            var passwordBox = (PasswordBox)sender;
            SetBoundPassword(passwordBox, passwordBox.Password);
        }
    }
}
