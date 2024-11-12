using LoginApp.DbContexts;
using LoginApp.Models;
using System.Text.RegularExpressions;

namespace LoginApp.Validators
{
    public partial class SignUpAccountValidator
    {
        private static readonly ToneProjectContext _dbContext = new();

        /// <summary>
        /// 아이디 입력 정규식
        /// </summary>
        [GeneratedRegex(@"^[a-zA-Z0-9]*$")]
        private static partial Regex IdRegex();

        /// <summary>
        /// 비밀번호 입력 정규식
        /// </summary>
        [GeneratedRegex(@"^[a-zA-Z](?=.*\d)(?=.*[!@#$%]).+$")]
        private static partial Regex PasswordRegex();

        /// <summary>
        /// 회원가입(계정) 성공 시 반환되는 결과
        /// </summary>
        public static readonly SignUpAccountResult SignUpAccountSuccess = new(true, string.Empty, string.Empty, string.Empty);

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
            else if (_dbContext.UserInfos.Any(u => u.UserId == userId))
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
        /// <returns>회원가입 결과를 나타내는 객체 반환</returns>
        public static SignUpAccountResult CheckSignUp(string userId, string password, string checkPassword)
        {
            string idStatus = CheckId(userId);
            string pwdStatus = CheckPassword(password);
            string pwdCheckStatus = DoubleCheckPassword(password, checkPassword);

            // 아이디, 비밀번호, 비밀번호 확인 메시지가 비었을 때
            bool isCorrect = string.IsNullOrEmpty(idStatus) && string.IsNullOrEmpty(pwdStatus) && string.IsNullOrEmpty(pwdCheckStatus);

            return isCorrect ? SignUpAccountSuccess : new(false, idStatus, pwdStatus, pwdCheckStatus);
        }
    }
}
