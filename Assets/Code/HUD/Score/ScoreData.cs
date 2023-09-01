using TMPro;

namespace Code.HUD.Score
{
    public struct ScoreData
    {
        public TextMeshProUGUI BestScoreText;
        public TextMeshProUGUI ScoreTextDefeatScreenScreen;
        public TextMeshProUGUI ScoreTextGameScreen;
        public int CurrentScore;
        public int BestScore;
        public string ScoreHashKey;
    }
}