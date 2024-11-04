using LoginApp.DbContexts;
using System.Text.RegularExpressions;

namespace LoginApp.Utils
{
    /// <summary>
    /// 로그인 및 회원가입과 관련된 입력값 검증 클래스
    /// </summary>
    public static partial class ValidationHelper
    {
        #region 로그인 관련 검증
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="password"></param>
        /// <param name="dbContext"></param>
        /// <returns></returns>
        public static (bool IsValid, string Message, bool ClearId, bool ClearPassword) ValidateSignInput(string id, string password, UserInfoContext dbContext)
        {
            if (string.IsNullOrEmpty(id))
                return (false, "아이디를 입력하세요", true, false);

            if (string.IsNullOrEmpty(password))
                return (false, "비밀번호를 입력하세요", false, true);

            if (!dbContext.UserInfos.Any(u => u.Id == id))
                return (false, "아이디 또는 비밀번호가 올바르지 않습니다", true, true);

            var user = dbContext.UserInfos.SingleOrDefault(u => u.Id == id);
            if (user == null || user.Pwd != password)
                return (false, "비밀번호가 올바르지 않습니다.", false, true);

            return (true, string.Empty, false, false);
        }
        #endregion

        #region 회원가입(계정) 관련 검증

        [GeneratedRegex(@"^[a-zA-Z0-9]*$")]
        private static partial Regex IdRegex();

        public static string ValidateUserId(string userId, UserInfoContext context)
        {
            int letterCount = userId.Count(char.IsLetter);

            if (string.IsNullOrEmpty(userId))
            {
                return "아이디를 입력해주세요.";
            }
            if (!IdRegex().IsMatch(userId))
            {
                return "아이디는 영문과 숫자만 입력 가능합니다.";
            }
            if (letterCount < 6)
            {
                return "영문자를 6자리 이상 입력해주세요.";
            }
            if (CheckUserIdExist(userId, context))
            {
                return "이미 존재하는 아이디입니다.";
            }
            return string.Empty;
        }

        [GeneratedRegex(@"^[a-zA-Z](?=.*\d)(?=.*[!@#$%]).+$")]
        private static partial Regex PasswordRegex();

        public static string ValidatePassword(string password)
        {
            int letterCount = password.Count(char.IsLetter);

            if (string.IsNullOrEmpty(password))
            {
                return "비밀번호를 입력해주세요.";
            }
            if (letterCount < 6)
            {
                return "영문자를 6자리 이상 입력해주세요.";
            }
            if (!PasswordRegex().IsMatch(password))
            {
                return "숫자, 특수문자(!,@,#,$,%)를 포함해야 합니다.";
            }
            return string.Empty;
        }

        public static string ValidateConfirmPassword(string password, string confirmPassword)
        {
            if (string.IsNullOrEmpty(confirmPassword))
            {
                return "비밀번호 확인이 필요합니다";
            }
            if (password != confirmPassword)
            {
                return "비밀번호가 일치하지 않습니다";
            }
            return string.Empty;
        }

        private static bool CheckUserIdExist(string userId, UserInfoContext context)
        {
            return context.UserInfos.Any(u => u.Id == userId);
        }

        public static (bool isValid, string IdStatus, string PasswordStatus, string ConfirmPasswordStatus) ValidateSignUpInput(
            string userId, string password, string confirmPassword, UserInfoContext context)
        {
            var idStatus = ValidateUserId(userId, context);
            var passwordStatus = ValidatePassword(password);
            var confirmPasswordStatus = ValidateConfirmPassword(password, confirmPassword);

            bool isValid = string.IsNullOrEmpty(idStatus) && string.IsNullOrEmpty(passwordStatus) && string.IsNullOrEmpty(confirmPasswordStatus);

            return (isValid, idStatus, passwordStatus, confirmPasswordStatus);
        }
        #endregion

        #region 회원가입(개인정보) 관련 검증
        /// <summary>
        /// 이름의 유효성을 검증합니다.
        /// </summary>
        /// <param name="name">이름</param>
        /// <returns>유효하지 않으면 오류 메시지를 반환하고, 유효하면 빈 문자열을 반환합니다.</returns>
        public static string ValidateName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return "이름을 입력해 주세요";
            }
            return string.Empty;
        }

        /// <summary>
        /// 생년월일의 유효성을 검증합니다.
        /// </summary>
        /// <param name="birth">생년월일</param>
        /// <returns>유효하지 않으면 오류 메시지를 반환하고, 유효하면 빈 문자열을 반환합니다.</returns>
        public static string ValidateBirth(string birth)
        {
            if (string.IsNullOrEmpty(birth))
            {
                return "생년월일을 입력해 주세요";
            }
            else if (birth.Length != 8 || !DateOnly.TryParseExact(birth, "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out _))
            {
                return "생년월일 형식에 맞는\n8자리 숫자로 입력해 주세요";
            }
            return string.Empty;
        }

        /// <summary>
        /// 회원가입 시 이름과 생년월일의 유효성을 검증하고 결과를 튜플로 반환합니다.
        /// </summary>
        /// <param name="name">이름</param>
        /// <param name="birth">생년월일</param>
        /// <returns>유효성 상태를 나타내는 튜플을 반환합니다.</returns>
        public static (bool isValid, string nameStatus, string birthStatus) ValidateSignUpInput(
            string name, string birth)
        {
            string nameStatus = ValidateName(name);
            string birthStatus = ValidateBirth(birth);

            bool isValid = string.IsNullOrEmpty(nameStatus) && string.IsNullOrEmpty(birthStatus);

            return (isValid, nameStatus, birthStatus);
        }
        #endregion
    }
}
