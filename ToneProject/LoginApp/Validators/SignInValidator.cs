using LoginApp.DbContexts;
using LoginApp.Models;
using LoginApp.ViewModels;

namespace LoginApp.Validators
{
    public class SignInValidator
    {
        private static readonly ToneProjectContext _dbContext = new();

        public static readonly SignInResult SignInSuccess = new(true, string.Empty, false, false);
        public static readonly SignInResult EmptyUserId = new(false, "아이디를 입력하세요", true, false);
        public static readonly SignInResult EmptyUserPwd = new(false, "비밀번호를 입력하세요", false, true);
        public static readonly SignInResult IncorrectIdOrPassword = new(false, "아이디 또는 비밀번호가 올바르지 않습니다", true, true);

        /// <summary>
        /// 로그인 입력 확인 메서드
        /// </summary>
        /// <param name="id">사용자 아이디</param>
        /// <param name="password">사용자 비밀번호</param>
        /// <returns>로그인 결과를 나타내는 객체 반환</returns>
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

            UserInfo? user = _dbContext.UserInfos.FirstOrDefault(u => u.UserId == id);

            if (user == null || user.Pwd != password)
            {
                return IncorrectIdOrPassword;
            }

            return SignInSuccess;
        }
    }
}
