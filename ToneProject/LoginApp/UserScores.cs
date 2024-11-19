using LoginApp.DbContexts;
using LoginApp.Models;
using Microsoft.EntityFrameworkCore;

namespace LoginApp
{
    /// <summary>
    /// 점수 데이터 조회 클래스
    /// </summary>
    public class UserScores
    {
        /// <summary>
        /// 접속 계정의 점수 데이터 조회
        /// </summary>
        /// <param name="userId">로그인 계정 아이디</param>
        /// <param name="topCount">조회 데이터 수</param>
        /// <returns>조건에 따라 조회된 데이터 반환, 데이터가 없으면 빈 리스트 반환</returns>
        public static async Task<List<SnakeGameHistory>> LoadMyTopScoresAsync(string userId, int topCount = 5)
        {
            await using var context = new ToneProjectContext();
            UserInfo? user = await context.UserInfos
                .Include(u => u.SnakeGameHistories)
                .FirstAsync(u => u.UserId == userId);

            // 데이터가 없으면 빈 리스트
            if (user.SnakeGameHistories == null || user.SnakeGameHistories.Count == 0)
            {
                return [];
            }

            return user.SnakeGameHistories
                .OrderByDescending(r => r.GameClear)
                .ThenByDescending(r => r.Score)
                .Take(topCount)
                .ToList();
        }
    }
}
