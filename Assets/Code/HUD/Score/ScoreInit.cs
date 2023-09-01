using Code.Constants;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Code.HUD.Score
{
    public class ScoreInit : IEcsInitSystem
    {
        private readonly EcsPoolInject<ScoreData> _scoreData = default;
        private readonly EcsCustomInject<ScoreSettings> _scoreSettings = default;
        private readonly int _bestScore;

        public ScoreInit()
        {
            if (PlayerPrefs.HasKey(GameConstants.BestScore))
            {
                _bestScore = PlayerPrefs.GetInt(GameConstants.BestScore);
            }
        }

        public void Init(IEcsSystems systems)
        {
            var entity = systems.GetWorld().NewEntity();
            ref var scoreData = ref _scoreData.Value.Add(entity);

            scoreData.ScoreHashKey = GameConstants.BestScore;
            scoreData.BestScoreText = _scoreSettings.Value.BestScoreText;
            scoreData.BestScore = _bestScore;
            scoreData.BestScoreText.text = _bestScore.ToString();
            scoreData.ScoreTextGameScreen = _scoreSettings.Value.ScoreTextGameScreen;
            scoreData.ScoreTextDefeatScreenScreen = _scoreSettings.Value.ScoreTextDefeatScreenScreen;
            scoreData.ScoreTextGameScreen.text = 0.ToString();
        }
    }
}