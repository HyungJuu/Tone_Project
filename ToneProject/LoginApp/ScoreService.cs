using LoginApp.DbContexts;
using LoginApp.Models;

namespace LoginApp
{
    /// <summary>
    /// 점수 데이터 조회 클래스
    /// </summary>
    public class ScoreService
    {
        /// <summary>
        /// 접속 계정의 점수 데이터 조회
        /// </summary>
        /// <param name="userId">로그인 계정 아이디</param>
        /// <param name="topCount">조회 데이터 수</param>
        /// <returns></returns>
        public static List<SnakeGameRecord> GetTopScoresByUserId(string userId, int topCount = 5)
        {
            using var context = new SnakeGameContext();
            return [.. context.SnakeGameRecords
                .Where(r => r.UserId == userId)
                .OrderByDescending(r => r.GameClear)
                .ThenByDescending(r => r.Score)
                .Take(topCount)];
        }
    }
}
