using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginApp.Utils
{
    public static class ValidationHelper
    {
        // 로그인 검증 메서드
        public static string ValidateSignInId(string id)
        {
            if (string.IsNullOrEmpty(id))
                return "아이디를 입력하세요";

            return null;
        }

        public static string ValidateSignInPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                return "비밀번호를 입력하세요";

            return null;
        }

        public static string ValidatePasswordMatch(string inputPassword, string actualPassword)
        {
            if (inputPassword != actualPassword)
                return "비밀번호가 올바르지 않습니다.";

            return null;
        }

        // 회원가입 검증 메서드
        public static string ValidateSignUpName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return "이름을 입력하세요";

            return null;
        }

        public static string ValidateSignUpBirth(string birth)
        {
            if (string.IsNullOrEmpty(birth))
                return "생년월일을 입력하세요";
            if (birth.Length != 8 || !DateOnly.TryParseExact(birth, "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out _))
                return "생년월일 형식에 맞는 8자리 숫자로 입력해 주세요";

            return null;
        }
    }
}
