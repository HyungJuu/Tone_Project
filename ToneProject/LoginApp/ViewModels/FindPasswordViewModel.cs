using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LoginApp.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace LoginApp.ViewModels
{
    /// <summary>
    /// 비밀번호 찾기 뷰모델 클래스.<br/>
    /// 사용자가 입력한 아이디, 생년월일을 기반으로 비밀번호 조회
    /// </summary>
    public partial class FindPasswordViewModel : ObservableObject
    {
        private static readonly ToneProjectContext _dbContext = new();

        /// <summary>
        /// 입력 아이디
        /// </summary>
        [ObservableProperty]
        private string _userId = "";

        /// <summary>
        /// 입력 생년월일
        /// </summary>
        [ObservableProperty]
        private string _userBirth = "";

        /// <summary>
        /// 조회된 비밀번호
        /// </summary>
        [ObservableProperty]
        private string _userPassword = "";

        /// <summary>
        /// 비밀번호 찾기 오류메시지 표시
        /// </summary>
        [ObservableProperty]
        private string _findPasswordStatus = "";

        /// <summary>
        /// 비밀번호를 찾는 비동기 메서드<br/>
        /// 입력한 아이디와 생년월일을 기반으로 사용자를 조회 후 비밀번호 표시
        /// </summary>
        /// /// <remarks>
        /// 테스트계정 : 아이디(admin), 비밀번호(admin), 이름(admin), 생년월일(19981010)
        /// </remarks>
        /// <returns></returns>
        [RelayCommand]
        public async Task FindPasswordAsync()
        {
            UserPassword = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(UserId) || string.IsNullOrEmpty(UserBirth))
                {
                    FindPasswordStatus = "필수 입력입니다.";
                }
                else if (UserBirth.Length != 8 || !DateOnly.TryParseExact(UserBirth, "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out var parsedBirth))
                {
                    FindPasswordStatus = "잘못된 입력입니다.";
                }
                else if (UserId == "admin" && UserBirth == "19981010")
                {
                    FindPasswordStatus = string.Empty;
                    UserPassword = $"비밀번호 : {MaskPassword("admin")}";
                }
                else
                {
                    Models.UserInfo? user = await _dbContext.UserInfos
                        .FirstOrDefaultAsync(u => u.UserId == UserId && u.Birth == parsedBirth);

                    if (user == null)
                    {
                        FindPasswordStatus = "사용자를 찾을 수 없습니다.";
                    }
                    else
                    {
                        FindPasswordStatus = string.Empty;
                        UserPassword = $"비밀번호 : {MaskPassword(user.Pwd)}";
                    }
                }
            }
            catch (Exception ex)
            {
                FindPasswordStatus = $"오류 발생: {ex.Message}";
            }
        }

        /// <summary>
        /// 조회된 비밀번호를 일부 암호화 처리<br/>
        /// 앞 4자리를 제외한 나머지는 '*' 처리
        /// </summary>
        /// <param name="password">사용자 비밀번호</param>
        /// <returns>암호화된 비밀번호</returns>
        private string MaskPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                return string.Empty;

            if (password.Length <= 4)
                return password;

            return password[..4] + new string('*', password.Length - 4);
        }
    }
}