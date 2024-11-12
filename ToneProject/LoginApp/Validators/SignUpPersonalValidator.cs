using LoginApp.ViewModels.SignUp;
using System.Text.RegularExpressions;

namespace LoginApp.Validators
{
    public partial class SignUpPersonalValidator
    {
        /// <summary>
        /// 이름 입력 정규식
        /// </summary>
        /// <returns></returns>
        [GeneratedRegex(@"^[a-zA-Z가-힣]+$")]
        private static partial Regex NameRegex();

        public static readonly SignUpPersonalResult SignUpPersonalSuccess = new(true, string.Empty, string.Empty);

        /// <summary>
        /// 이름 입력 상태 확인 메서드
        /// </summary>
        /// <param name="name">사용자 입력 이름</param>
        /// <returns>조건에 따른 문자열(오류메시지) 반환. 아니면 빈문자열 반환</returns>
        private static string CheckName(string name)
        {
            string inputNameResult = string.Empty;

            if (string.IsNullOrEmpty(name))
            {
                inputNameResult = "이름을 입력해 주세요";
            }
            else if (!NameRegex().IsMatch(name))
            {
                inputNameResult = "영문과 한글만 입력 가능합니다";
            }
            else
            {
                return inputNameResult;
            }
            return inputNameResult;
        }

        /// <summary>
        /// 생년월일 입력 상태 확인 메서드
        /// </summary>
        /// <param name="birth">사용자 입력 생년월일</param>
        /// <returns>조건에 따른 문자열(오류메시지) 반환. 아니면 빈문자열 반환</returns>
        private static string CheckBirth(string birth)
        {
            string inputBirthResult = string.Empty;

            if (string.IsNullOrEmpty(birth))
            {
                inputBirthResult = "생년월일을 입력해 주세요";
            }
            else if (birth.Length != 8 || !DateOnly.TryParseExact(birth, "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out _))
            {
                inputBirthResult = "생년월일 형식에 맞는\n8자리 숫자로 입력해 주세요";
            }
            else
            {
                return inputBirthResult;
            }
            return inputBirthResult;
        }

        /// <summary>
        /// 회원가입(개인정보) 입력 확인 메서드
        /// </summary>
        /// <param name="name">사용자 입력 이름</param>
        /// <param name="birth">사용자 입력 생년월일</param>
        /// <returns>회원가입 결과를 나타내는 객체 반환</returns>
        public static SignUpPersonalResult CheckSignUp(string name, string birth)
        {
            string nameStatus = CheckName(name);
            string birthStatus = CheckBirth(birth);

            bool isCorrect = string.IsNullOrEmpty(nameStatus) && string.IsNullOrEmpty(birthStatus);

            return isCorrect ? SignUpPersonalSuccess : new(false, nameStatus, birthStatus);
        }
    }
}
