using System.Linq;
using LoginApp.DbContexts;
using LoginApp.Models;

namespace LoginApp.Utils
{
    public static class ValidationHelper
    {
        #region 로그인 관련 검증
        /// <summary>
        /// 아이디가 비어 있는지 확인하고, 필요한 메시지를 반환합니다.
        /// </summary>
        public static string CheckIdNotEmpty(string id)
        {
            return string.IsNullOrEmpty(id) ? "아이디를 입력하세요" : string.Empty;
        }

        /// <summary>
        /// 비밀번호가 비어 있는지 확인하고, 필요한 메시지를 반환합니다.
        /// </summary>
        public static string CheckPasswordNotEmpty(string password)
        {
            return string.IsNullOrEmpty(password) ? "비밀번호를 입력하세요" : string.Empty;
        }

        /// <summary>
        /// 입력된 아이디가 데이터베이스에 존재하는지 확인합니다.
        /// </summary>
        public static bool CheckIdExists(string id, UserInfoContext dbContext)
        {
            return dbContext.UserInfos.Any(u => u.Id == id);
        }

        /// <summary>
        /// 아이디와 비밀번호가 데이터베이스의 정보와 일치하는지 확인합니다.
        /// </summary>
        public static bool CheckPasswordMatch(string id, string password, UserInfoContext dbContext)
        {
            var user = dbContext.UserInfos.FirstOrDefault(u => u.Id == id);
            return user != null && user.Pwd == password;
        }

        /// <summary>
        /// 전체 로그인 정보를 검증하고, 검증 결과 메시지를 반환합니다.
        /// </summary>
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
        /// 아이디의 유효성을 검증하고, 실패 시 메시지를 반환합니다.
        /// </summary>
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
        /// 비밀번호의 유효성을 검증하고, 실패 시 메시지를 반환합니다.
        /// </summary>
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
        /// 비밀번호 확인란의 유효성을 검증하고, 실패 시 메시지를 반환합니다.
        /// </summary>
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
        /// 데이터베이스에서 아이디가 존재하는지 확인합니다.
        /// </summary>
        private static bool DoesUserIdExist(string userId, UserInfoContext context)
        {
            return context.UserInfos.Any(u => u.Id == userId);
        }

        /// <summary>
        /// 회원가입 전체 검증을 수행하고, 각 상태 메시지를 업데이트합니다.
        /// </summary>
        public static (string IdStatus, string PasswordStatus, string ConfirmPasswordStatus) ValidateSignUpInput(
            string userId, string password, string confirmPassword, UserInfoContext context)
        {
            var idStatus = ValidateUserId(userId, context);
            var passwordStatus = ValidatePassword(password);
            var confirmPasswordStatus = ValidateConfirmPassword(password, confirmPassword);

            return (idStatus, passwordStatus, confirmPasswordStatus);
        }
        #endregion

        #region 회원가입(개인정보) 관련 검증

        #endregion
    }
}
