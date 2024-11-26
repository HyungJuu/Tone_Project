using LoginApp.Models;

namespace LoginApp.ViewModels.SnakeGame
{
    /// <summary>
    /// admin 테스트 계정의 게임기록 관리 클래스
    /// </summary>
    public static class AdminScores
    {
        /// <summary>
        /// 테스트용 임의 데이터
        /// </summary>
        private static readonly List<SnakeGameHistory> adminScores =
        [
            new SnakeGameHistory
            {
                UserId = "user",
                PlayedDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-1)),
                GameClear = true,
                PlayTime = 60,
                Score = 20
            },
            new SnakeGameHistory
            {
                UserId = "test",
                PlayedDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-2)),
                GameClear = true,
                PlayTime = 60,
                Score = 21
            },
            new SnakeGameHistory
            {
                UserId = "test",
                PlayedDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-4)),
                GameClear = false,
                PlayTime = 20,
                Score = 7
            }
        ];

        /// <summary>
        /// 테스트 계정의 게임 기록 조회<br/>
        /// 성공여부 및 점수로 정렬
        /// </summary>
        /// <returns>정렬된 리스트</returns>
        public static List<SnakeGameHistory> GetAdminScores(int topCount = 5)
        {
            return [.. adminScores
                .Where(h => h.UserId == "admin")
                .OrderByDescending(h => h.GameClear)
                .ThenByDescending(h => h.Score)
                .Take(topCount)];
        }

        /// <summary>
        /// 데이터베이스 X<br/>
        /// 임의의 전체 게임 기록 조회
        /// </summary>
        /// <returns>정렬된 리스트</returns>
        public static List<SnakeGameHistory> GetTotalScores(int topCount = 5)
        {
            return [.. adminScores
                .OrderByDescending(h => h.GameClear)
                .ThenByDescending(h => h.Score)
                .Take(topCount)];
        }

        /// <summary>
        /// 테스트 계정의 게임기록 리스트에 데이터 추가
        /// </summary>
        /// <param name="history">추가할 게임 기록</param>
        public static void AddAdminScore(SnakeGameHistory history)
        {
            adminScores.Add(history);
        }
    }
}