using System;
using System.Globalization;
using System.Windows.Data;

namespace LoginApp.Converters
{
    /// <summary>
    /// 문자열과 불리언 값을 상호변환하는 변환기
    /// </summary>
    public class StringToBooleanConverter : IValueConverter
    {
        /// <summary>
        /// 주어진 문자열과 파라미터 문자열이 같은지를 비교하여 불리언 값을 반환합니다.
        /// </summary>
        /// <param name="value">변환할 문자열 값입니다.</param>
        /// <param name="targetType">변환하려는 대상 타입입니다.</param>
        /// <param name="parameter">비교할 문자열입니다.</param>
        /// <param name="culture">문화권 정보입니다.</param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value?.ToString() == parameter?.ToString();
        }

        /// <summary>
        /// 불리언 값이 true인 경우 파라미터의 문자열을 반환하고, false인 경우 Binding.DoNothing을 반환합니다.
        /// </summary>
        /// <param name="value">변환할 불리언 값입니다.</param>
        /// <param name="targetType">변환하려는 대상 타입입니다.</param>
        /// <param name="parameter">반환할 문자열입니다.</param>
        /// <param name="culture">문화권 정보입니다.</param>
        /// <returns>불리언 값이 true일 경우 파라미터의 문자열, false일 경우 Binding.DoNothing을 반환합니다.</returns>
        public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? parameter?.ToString() : Binding.DoNothing;
        }
    }
}
