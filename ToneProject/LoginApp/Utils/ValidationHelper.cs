using System.Linq;
using LoginApp.DbContexts;
using LoginApp.Models;
using LoginApp.ViewModels;

namespace LoginApp.Utils
{
    /// <summary>
    /// 로그인 및 회원가입과 관련된 입력값 검증 클래스
    /// </summary>
    public static class ValidationHelper
    {
        #region 로그인 관련 검증
        /// <summary>
        /// 아이디가 비어있는지 확인합니다.
        /// </summary>
        /// <param name="id">검증할 아이디</param>
        /// <returns>아이디가 비어 있으면 오류 메시지, 그렇지 않으면 빈 문자열을 반환합니다.</returns>
        public static string CheckIdNotEmpty(string id)
        {
            return string.IsNullOrEmpty(id) ? "아이디를 입력하세요" : string.Empty;
        }

        /// <summary>
        /// 비밀번호가 비어 있는지 확인합니다.
        /// </summary>
        /// <param name="password">검증할 비밀번호</param>
        /// <returns>비밀번호가 비어 있으면 오류 메시지, 그렇지 않으면 빈 문자열을 반환합니다.</returns>
        public static string CheckPasswordNotEmpty(string password)
        {
            return string.IsNullOrEmpty(password) ? "비밀번호를 입력하세요" : string.Empty;
        }

        /// <summary>
        /// 아이디가 데이터베이스에 존재하는지 확인합니다.
        /// </summary>
        /// <param name="id">검증할 아이디</param>
        /// <param name="dbContext">데이터베이스 컨텍스트</param>
        /// <returns>아이디가 존재하면 true, 그렇지 않으면 false 반환합니다.</returns>
        public static bool CheckIdExists(string id, UserInfoContext dbContext)
        {
            return dbContext.UserInfos.Any(u => u.Id == id);
        }

        /// <summary>
        /// 아이디와 비밀번호가 데이터베이스의 정보와 일치하는지 확인합니다.
        /// </summary>
        /// <param name="id">아이디</param>
        /// <param name="password">비밀번호</param>
        /// <param name="dbContext">데이터베이스 컨텍스트</param>
        /// <returns>아이디와 비밀번호가 일치하면 true, 그렇지 않으면 false 반환합니다.</returns>
        public static bool CheckPasswordMatch(string id, string password, UserInfoContext dbContext)
        {
            var user = dbContext.UserInfos.FirstOrDefault(u => u.Id == id);
            return user != null && user.Pwd == password;
        }

        /// <summary>
        /// 로그인 자격 증명을 검증하고 적절한 오류 메시지를 반환합니다.
        /// </summary>
        /// <param name="id">아이디</param>
        /// <param name="password">비밀번호</param>
        /// <param name="dbContext">데이터베이스 컨텍스트</param>
        /// <returns>자격 증명이 올바르지 않으면 오류 메시지를 반환하고, 올바르면 빈 문자열을 반환합니다.</returns>
        public static string ValidateSignInCredentials(string id, string password, UserInfoContext dbContext)
        {
            var idEmptyMessage = CheckIdNotEmpty(id);
            if (!string.IsNullOrEmpty(idEmptyMessage))
                return idEmptyMessage;

            var passwordEmptyMessage = CheckPasswordNotEmpty(password);
            if (!string.IsNullOrEmpty(passwordEmptyMessage))
                return passwordEmptyMessage;

            if (!CheckIdExists(id, dbContext))
                return "아이디 또는 비밀번호가 올바르지 않습니다";

            if (!CheckPasswordMatch(id, password, dbContext))
                return "비밀번호가 올바르지 않습니다.";

            return string.Empty;
        }
        #endregion


        #region 회원가입(계정) 관련 검증
        /// <summary>
        /// 사용자 아이디의 유효성을 검증합니다.
        /// </summary>
        /// <param name="userId">검증할 사용자 아이디</param>
        /// <param name="context">데이터베이스 컨텍스트</param>
        /// <returns>유효하지 않으면 오류 메시지를 반환하고, 유효하면 빈 문자열을 반환합니다.</returns>
        public static string ValidateUserId(string userId, UserInfoContext context)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return "아이디를 입력해주세요";
            }
            if (userId.Length < 3)
            {
                return "아이디를 3자리 이상 입력해주세요";
            }
            if (DoesUserIdExist(userId, context))
            {
                return "이미 존재하는 아이디입니다.";
            }
            return string.Empty;
        }

        /// <summary>
        /// 비밀번호의 유효성을 검증합니다.
        /// </summary>
        /// <param name="password">검증할 비밀번호</param>
        /// <returns>유효하지 않으면 오류 메시지를 반환하고, 유효하면 빈 문자열을 반환합니다.</returns>
        public static string ValidatePassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return "비밀번호를 입력해주세요";
            }
            if (password.Length < 4)
            {
                return "비밀번호를 4자리 이상 입력해주세요";
            }
            return string.Empty;
        }

        /// <summary>
        /// 비밀번호 확인의 유효성을 검증합니다.
        /// </summary>
        /// <param name="password">비밀번호</param>
        /// <param name="confirmPassword">비밀번호 확인</param>
        /// <returns>비밀번호가 일치하지 않으면 오류 메시지를 반환하고, 일치하면 빈 문자열을 반환합니다.</returns>
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

        /// <summary>
        /// 주어진 아이디가 데이터베이스에 존재하는지 확인합니다.
        /// </summary>
        /// <param name="userId">아이디</param>
        /// <param name="context">데이터베이스 컨텍스트</param>
        /// <returns>아이디가 존재하면 true, 그렇지 않으면 false 반환합니다.</returns>
        private static bool DoesUserIdExist(string userId, UserInfoContext context)
        {
            return context.UserInfos.Any(u => u.Id == userId);
        }

        /// <summary>
        /// 회원가입 시 아이디, 비밀번호, 비밀번호 확인의 유효성을 검증하고 결과를 튜플로 반환합니다.
        /// </summary>
        /// <param name="userId">아이디</param>
        /// <param name="password">비밀번호</param>
        /// <param name="confirmPassword">비밀번호 확인</param>
        /// <param name="context">데이터베이스 컨텍스트</param>
        /// <returns>유효성 상태를 나타내는 튜플을 반환합니다.</returns>
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
