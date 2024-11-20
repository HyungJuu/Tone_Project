using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LoginApp.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace LoginApp.ViewModels
{
    /// <summary>
    /// 아이디 찾기 뷰모델 클래스<br/>
    /// 사용자 이름, 생년월일을 기반으로 아이디 조회
    /// </summary>
    public partial class FindIdViewModel : ObservableObject
    {
        private static readonly ToneProjectContext _dbContext = new();

        /// <summary>
        /// 입력 이름
        /// </summary>
        [ObservableProperty]
        private string _userName = "";

        /// <summary>
        /// 입력 생년월일
        /// </summary>
        [ObservableProperty]
        private string _userBirth = "";

        /// <summary>
        /// 조회된 아이디
        /// </summary>
        [ObservableProperty]
        private string _userId = "";

        /// <summary>
        /// 아이디 찾기 오류메시지 표시
        /// </summary>
        [ObservableProperty]
        private string _findIdStatus = "";

        /// <summary>
        /// 아이디를 찾는 비동기 메서드<br/>
        /// 입력한 이름과 생년월일을 기반으로 사용자를 조회 후 아이디 표시
        /// </summary>
        /// <remarks>
        /// 테스트계정 : 아이디(admin), 비밀번호(admin), 이름(admin), 생년월일(19981010)
        /// </remarks>
        /// <returns></returns>
        [RelayCommand]
        public async Task FindIdAsync()
        {
            UserId = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(UserBirth))
                {
                    FindIdStatus = "필수 입력입니다.";
                }
                else if (UserBirth.Length != 8 || !DateOnly.TryParseExact(UserBirth, "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out var parsedBirth))
                {
                    FindIdStatus = "잘못된 입력입니다.";
                }
                else if (UserName == "admin" && UserBirth == "19981010")
                {
                    FindIdStatus = string.Empty;
                    UserId = $"아이디 : {"admin"}";
                }
                else
                {
                    Models.UserInfo? user = await _dbContext.UserInfos
                        .FirstOrDefaultAsync(u => u.Name == UserName && u.Birth == parsedBirth);

                    if (user == null)
                    {
                        FindIdStatus = "사용자를 찾을 수 없습니다.";
                    }
                    else
                    {
                        FindIdStatus = string.Empty;
                        UserId = $"아이디 : {(user.UserId)}";
                    }
                }
            }
            catch (Exception ex)
            {
                FindIdStatus = $"오류 발생: {ex.Message}";
            }
        }
    }
}