using LoginApp.DbContexts;
using System.Text.RegularExpressions;

namespace LoginApp.Utils
{
    public static partial class ValidationHelper
    {
        private static readonly UserInfoContext _dbContext = new();

        #region 로그인 관련

        /// <summary>
        /// 로그인 결과를 나타내는 클래스
        /// </summary>
        /// <param name="isValid">로그인 성공 유무</param>
        /// <param name="message">오류메시지</param>
        /// <param name="clearId">아이디 입력 초기화 유무</param>
        /// <param name="clearPassword">비밀번호 입력 초기화 유무</param>
        public struct SignInResult(bool isValid, string message, bool clearId, bool clearPassword)
        {
            public bool IsValid { get; set; } = isValid;
            public string Message { get; set; } = message;
            public bool ClearId { get; set; } = clearId;
            public bool ClearPassword { get; set; } = clearPassword;
        }

        /// <summary>
        /// 로그인 입력 확인 메서드
        /// </summary>
        /// <param name="id">사용자 아이디</param>
        /// <param name="password">사용자 비밀번호</param>
        /// <returns></returns>
        public static SignInResult CheckSignIn(string id, string password)
        {
            if (string.IsNullOrEmpty(id))
                return new SignInResult(false, "아이디를 입력하세요", true, false);

            if (string.IsNullOrEmpty(password))
                return new SignInResult(false, "비밀번호를 입력하세요", false, true);

            var user = _dbContext.UserInfos.SingleOrDefault(u => u.Id == id);

            if (user == null || user.Pwd != password)
                return new SignInResult(false, "아이디 또는 비밀번호가 올바르지 않습니다", true, true);

            return new SignInResult(true, string.Empty, false, false);
        }
        #endregion

        #region 회원가입(계정) 관련 검증

        [GeneratedRegex(@"^[a-zA-Z0-9]*$")]
        private static partial Regex IdRegex();

        public static string CheckUserId(string userId)
        {
            int letterCount = userId.Count(char.IsLetter);

            if (string.IsNullOrEmpty(userId))
                return "아이디를 입력해주세요.";

            if (!IdRegex().IsMatch(userId))
                return "아이디는 영문과 숫자만 입력 가능합니다.";

            if (letterCount < 4)
                return "영문자를 4자리 이상 입력해주세요.";

            if (CheckUserIdExist(userId))
                return "이미 존재하는 아이디입니다.";

            return string.Empty;
        }

        [GeneratedRegex(@"^[a-zA-Z](?=.*\d)(?=.*[!@#$%]).+$")]
        private static partial Regex PasswordRegex();

        public static string CheckPasswordMsg(string password)
        {
            int letterCount = password.Count(char.IsLetter);

            if (string.IsNullOrEmpty(password))
                return "비밀번호를 입력해주세요.";

            if (letterCount < 6)
                return "영문자를 6자리 이상 입력해주세요.";

            if (!PasswordRegex().IsMatch(password))
                return "숫자, 특수문자(!,@,#,$,%)를 포함해야 합니다.";
            
            return string.Empty;
        }

        public static string DoubleCheckPasswordMsg(string password, string confirmPassword)
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

        
        private static bool CheckUserIdExist(string userId)
        {
            return _dbContext.UserInfos.Any(u => u.Id == userId);
        }

        public static (bool isValid, string IdStatus, string PasswordStatus, string ConfirmPasswordStatus) ValidateSignUpInput(
            string userId, string password, string confirmPassword)
        {
            string idStatus = CheckUserId(userId);
            string passwordStatus = CheckPasswordMsg(password);
            string confirmPasswordStatus = DoubleCheckPasswordMsg(password, confirmPassword);

            bool isValid = string.IsNullOrEmpty(idStatus) && string.IsNullOrEmpty(passwordStatus) && string.IsNullOrEmpty(confirmPasswordStatus);

            return (isValid, idStatus, passwordStatus, confirmPasswordStatus);
        }
        #endregion

        #region 회원가입(개인정보) 관련 검증
        
        public static string ValidateName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return "이름을 입력해 주세요";
            }
            return string.Empty;
        }

        
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
