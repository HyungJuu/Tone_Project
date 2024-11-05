using LoginApp.DbContexts;
using LoginApp.Models;
using System.Text.RegularExpressions;

namespace LoginApp.Utils
{
    public static partial class ValidationHelper
    {
        private static readonly UserInfoContext _dbContext = new();

        #region 로그인 관련

        /// <summary>
        /// 로그인 결과를 나타내는 구조체
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
        /// <returns>로그인 결과를 나타내는 구조체 반환</returns>
        public static SignInResult CheckSignIn(string id, string password)
        {
            if (string.IsNullOrEmpty(id))
                return new SignInResult(false, "아이디를 입력하세요", true, false);

            if (string.IsNullOrEmpty(password))
                return new SignInResult(false, "비밀번호를 입력하세요", false, true);

            UserInfo? user = _dbContext.UserInfos.SingleOrDefault(u => u.Id == id);

            if (user == null || user.Pwd != password)
                return new SignInResult(false, "아이디 또는 비밀번호가 올바르지 않습니다", true, true);

            return new SignInResult(true, string.Empty, false, false);
        }
        #endregion

        #region 회원가입(계정) 관련 검증

        /// <summary>
        /// 회원가입(계정) 결과를 나타내는 구조체
        /// </summary>
        /// <param name="isValid">계정 입력 성공 유무</param>
        /// <param name="idStatus">아이디 입력 상태메시지</param>
        /// <param name="pwdStatus">비밀번호 입력 상태메시지</param>
        /// <param name="pwdCheckStatus">비밀번호 입력 확인 상태메시지</param>
        public struct SignUpAccountResult(bool isValid, string idStatus, string pwdStatus, string pwdCheckStatus)
        {
            public bool IsValid { get; set; } = isValid;
            public string IdStatus { get; set; } = idStatus;
            public string PwdStatus { get; set; } = pwdStatus;
            public string PwdCheckStatus { get; set; } = pwdCheckStatus;
        }

        /// <summary>
        /// 아이디 입력 정규식
        /// </summary>
        [GeneratedRegex(@"^[a-zA-Z0-9]*$")]
        private static partial Regex IdRegex();

        /// <summary>
        /// 아이디 입력 상태 확인 메서드
        /// </summary>
        /// <param name="userId">사용자 입력 아이디</param>
        /// <returns>조건에 따른 문자열(오류메시지) 반환. 아니면 빈문자열 반환</returns>
        private static string CheckId(string userId)
        {
            int letterCount = userId.Count(char.IsLetter);

            if (string.IsNullOrEmpty(userId))
                return "아이디를 입력해주세요.";

            if (!IdRegex().IsMatch(userId))
                return "아이디는 영문과 숫자만 입력 가능합니다.";

            if (letterCount < 4)
                return "영문자를 4자리 이상 입력해주세요.";

            if (_dbContext.UserInfos.Any(u => u.Id == userId))
                return "이미 존재하는 아이디입니다.";

            return string.Empty;
        }

        /// <summary>
        /// 비밀번호 입력 정규식
        /// </summary>
        [GeneratedRegex(@"^[a-zA-Z](?=.*\d)(?=.*[!@#$%]).+$")]
        private static partial Regex PasswordRegex();

        /// <summary>
        /// 비밀번호 입력 상태 확인 메서드
        /// </summary>
        /// <param name="password">사용자 입력 비밀번호</param>
        /// <returns>조건에 따른 문자열(오류메시지) 반환. 아니면 빈문자열 반환</returns>
        private static string CheckPassword(string password)
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

        /// <summary>
        /// 비밀번호 재입력 상태 확인 메서드
        /// </summary>
        /// <param name="password">사용자 입력 비밀번호</param>
        /// <param name="checkPassword">재입력 비밀번호</param>
        /// <returns>조건에 따른 문자열(오류메시지) 반환. 아니면 빈문자열 반환</returns>
        private static string DoubleCheckPassword(string password, string checkPassword)
        {
            if (string.IsNullOrEmpty(checkPassword))
            {
                return "비밀번호 확인이 필요합니다";
            }
            if (password != checkPassword)
            {
                return "비밀번호가 일치하지 않습니다";
            }
            return string.Empty;
        }

        /// <summary>
        /// 회원가입(계정) 입력 확인 메서드
        /// </summary>
        /// <param name="userId">사용자 입력 아이디</param>
        /// <param name="password">사용자 입력 비밀번호</param>
        /// <param name="checkPassword">사용자 입력 비밀번호 확인</param>
        /// <returns>회원가입 결과를 나타내는 구조체 반환</returns>
        public static SignUpAccountResult CheckSignUp(string userId, string password, string checkPassword)
        {
            string idStatus = CheckId(userId);
            string pwdStatus = CheckPassword(password);
            string pwdCheckStatus = DoubleCheckPassword(password, checkPassword);

            // 아이디, 비밀번호, 비밀번호 확인 메시지가 비었을 때
            bool isValid = string.IsNullOrEmpty(idStatus) && string.IsNullOrEmpty(pwdStatus) && string.IsNullOrEmpty(pwdCheckStatus);

            return new SignUpAccountResult(isValid, idStatus, pwdStatus, pwdCheckStatus);
        }
        #endregion

        #region 회원가입(개인정보) 관련 검증

        /// <summary>
        /// 회원가입(개인정보) 결과를 나타내는 구조체
        /// </summary>
        /// <param name="isValid">개인정보 입력 성공 유무</param>
        /// <param name="nameStatus">이름(닉네임) 입력 상태메시지</param>
        /// <param name="birthStatus">생년월일 입력 상태메시지</param>
        public struct SignUpPersonalResult(bool isValid, string nameStatus, string birthStatus)
        {
            public bool IsVaild { get; private set; } = isValid;
            public string NameStatus { get; private set; } = nameStatus;
            public string BirthStatus { get; private set; } = birthStatus;
        }

        /// <summary>
        /// 이름(닉네임) 입력 상태 확인 메서드
        /// </summary>
        /// <param name="name">사용자 입력 이름</param>
        /// <returns>조건에 따른 문자열(오류메시지) 반환. 아니면 빈문자열 반환</returns>
        private static string CheckName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return "이름을 입력해 주세요";
            }
            return string.Empty;
        }

        /// <summary>
        /// 생년월일 입력 상태 확인 메서드
        /// </summary>
        /// <param name="birth">사용자 입력 생년월일</param>
        /// <returns>조건에 따른 문자열(오류메시지) 반환. 아니면 빈문자열 반환</returns>
        private static string CheckBirth(string birth)
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
        /// 회원가입(개인정보) 입력 확인 메서드
        /// </summary>
        /// <param name="name">사용자 입력 이름</param>
        /// <param name="birth">사용자 입력 생년월일</param>
        /// <returns>회원가입 결과를 나타내는 구조체 반환</returns>
        public static SignUpPersonalResult CheckSignUp(string name, string birth)
        {
            string nameStatus = CheckName(name);
            string birthStatus = CheckBirth(birth);

            bool isValid = string.IsNullOrEmpty(nameStatus) && string.IsNullOrEmpty(birthStatus);

            return new SignUpPersonalResult(isValid, nameStatus, birthStatus);
        }
        #endregion
    }
}
