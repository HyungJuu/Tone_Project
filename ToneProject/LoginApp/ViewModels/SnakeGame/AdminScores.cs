using LoginApp.Models;

namespace LoginApp.ViewModels.SnakeGame
{
    /// <summary>
    /// admin 테스트 계정의 게임기록 관리 클래스
    /// </summary>
    public static class AdminScores
    {
        private static readonly List<SnakeGameHistory> adminScores = [];

        /// <summary>
        /// 테스트 계정의 게임 기록 조회<br/>
        /// 성공여부 및 점수로 정렬
        /// </summary>
        /// <returns>정렬된 리스트</returns>
        public static List<SnakeGameHistory> GetAdminScores()
        {
            return [.. adminScores
                .OrderByDescending(h => h.GameClear)
                .ThenByDescending(h => h.Score)];
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