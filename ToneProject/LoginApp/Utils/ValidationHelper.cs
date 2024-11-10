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
        /// 로그인 성공 시 반환되는 결과
        /// </summary>
        public static readonly SignInResult SignInSuccess = new(true, string.Empty, false, false);
        /// <summary>
        /// 아이디 미입력 시 반환되는 결과
        /// </summary>
        public static readonly SignInResult EmptyUserId = new(false, "아이디를 입력하세요", true, false);
        /// <summary>
        /// 비밀번호 미입력 시 반환되는 결과
        /// </summary>
        public static readonly SignInResult EmptyUserPwd = new(false, "비밀번호를 입력하세요", false, true);
        /// <summary>
        /// 아이디 또는 비밀번호가 일치하지 않을 시 반환되는 결과
        /// </summary>
        public static readonly SignInResult IncorrectIdOrPassword = new(false, "아이디 또는 비밀번호가 올바르지 않습니다", true, true);

        /// <summary>
        /// 로그인 입력 확인 메서드
        /// </summary>
        /// <param name="id">사용자 아이디</param>
        /// <param name="password">사용자 비밀번호</param>
        /// <returns>로그인 결과를 나타내는 구조체 반환</returns>
        public static SignInResult CheckSignIn(string id, string password)
        {
            if (string.IsNullOrEmpty(id))
            {
                return EmptyUserId;
            }

            if (string.IsNullOrEmpty(password))
            {
                return EmptyUserPwd;
            }

            UserInfo? user = _dbContext.UserInfos.SingleOrDefault(u => u.Id == id);

            if (user == null || user.Pwd != password)
            {
                return IncorrectIdOrPassword;
            }

            return SignInSuccess;
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
        /// 회원가입(계정) 성공 시 반환되는 결과
        /// </summary>
        public static readonly SignUpAccountResult SignUpAccountSuccess = new(true, string.Empty, string.Empty, string.Empty);

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
            string inputIdResult = string.Empty;

            if (string.IsNullOrEmpty(userId))
            {
                inputIdResult = "아이디를 입력해주세요.";
            }
            else if (!IdRegex().IsMatch(userId))
            {
                inputIdResult = "아이디는 영문과 숫자만 입력 가능합니다.";
            }
            else if (letterCount < 4)
            {
                inputIdResult = "영문자를 4자리 이상 입력해주세요.";
            }
            else if (_dbContext.UserInfos.Any(u => u.Id == userId))
            {
                inputIdResult = "이미 존재하는 아이디입니다.";
            }
            else
            {
                return inputIdResult;
            }

            return inputIdResult;
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
            string inputPwdResult = string.Empty;

            if (string.IsNullOrEmpty(password))
            {
                inputPwdResult = "비밀번호를 입력해주세요.";
            }
            else if (letterCount < 6)
            {
                inputPwdResult = "영문자를 6자리 이상 입력해주세요.";
            }
            else if (!PasswordRegex().IsMatch(password))
            {
                inputPwdResult = "숫자, 특수문자(!,@,#,$,%)를 포함해야 합니다.";
            }
            else
            {
                return inputPwdResult;
            }

            return inputPwdResult;
        }

        /// <summary>
        /// 비밀번호 재입력 상태 확인 메서드
        /// </summary>
        /// <param name="password">사용자 입력 비밀번호</param>
        /// <param name="checkPassword">재입력 비밀번호</param>
        /// <returns>조건에 따른 문자열(오류메시지) 반환. 아니면 빈문자열 반환</returns>
        private static string DoubleCheckPassword(string password, string checkPassword)
        {
            string inputPwdCheckResult = string.Empty;

            if (string.IsNullOrEmpty(checkPassword))
            {
                inputPwdCheckResult = "비밀번호 확인이 필요합니다";
            }
            else if (password != checkPassword)
            {
                inputPwdCheckResult = "비밀번호가 일치하지 않습니다";
            }
            else
            {
                return inputPwdCheckResult;
            }

            return inputPwdCheckResult;
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
            bool isCorrect = string.IsNullOrEmpty(idStatus) && string.IsNullOrEmpty(pwdStatus) && string.IsNullOrEmpty(pwdCheckStatus);

            return isCorrect ? SignUpAccountSuccess : new(false, idStatus, pwdStatus, pwdCheckStatus);
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
        /// 회원가입(개인정보) 성공 시 반환되는 결과
        /// </summary>
        public static readonly SignUpPersonalResult SignUpPersonalSuccess = new(true, string.Empty, string.Empty);

        /// <summary>
        /// 이름 입력 정규식
        /// </summary>
        /// <returns></returns>
        [GeneratedRegex(@"^[a-zA-Z가-힣]+$")]
        private static partial Regex nameRegex();

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
            else if (!nameRegex().IsMatch(name))
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
        /// <returns>회원가입 결과를 나타내는 구조체 반환</returns>
        public static SignUpPersonalResult CheckSignUp(string name, string birth)
        {
            string nameStatus = CheckName(name);
            string birthStatus = CheckBirth(birth);

            bool isCorrect = string.IsNullOrEmpty(nameStatus) && string.IsNullOrEmpty(birthStatus);

            return isCorrect ? SignUpPersonalSuccess : new(false, nameStatus, birthStatus);
        }
        #endregion
    }
}
