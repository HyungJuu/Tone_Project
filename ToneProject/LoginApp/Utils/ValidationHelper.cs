using System.Linq;
using LoginApp.DbContexts;
using LoginApp.Models;

namespace LoginApp.Utils
{
    public static class ValidationHelper
    {
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
    }
}
