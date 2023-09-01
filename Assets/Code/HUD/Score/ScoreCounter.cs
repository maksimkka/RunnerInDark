using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using TMPro;
using UnityEngine;

namespace Code.HUD.Score
{
    public class ScoreCounter : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<ScoreData>> _scoreFilter = default;

        private readonly float scoreIncreaseRate = 3f;
        private float timeSinceLastIncrease;
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _scoreFilter.Value)
            {
                ref var scoreData = ref _scoreFilter.Pools.Inc1.Get(entity);
                ChangeCurrentScore(ref scoreData);
            }
        }

        private void ChangeCurrentScore(ref ScoreData scoreData)
        {
            timeSinceLastIncrease += Time.deltaTime;

            if (timeSinceLastIncrease >= 1f / scoreIncreaseRate)
            {
                scoreData.CurrentScore += 1;
                scoreData.ScoreTextGameScreen.text = scoreData.CurrentScore.ToString();
                scoreData.ScoreTextDefeatScreenScreen.text = scoreData.CurrentScore.ToString();

                if (scoreData.CurrentScore > scoreData.BestScore)
                {
                    SavePlayerPrefs(scoreData.ScoreHashKey, scoreData.CurrentScore, scoreData.BestScoreText);
                }
                
                timeSinceLastIncrease = 0f;
            }
        }
        
        private void SavePlayerPrefs(string key, int value, TextMeshProUGUI changedText)
        {
            changedText.text = value.ToString();
            PlayerPrefs.SetInt(key, value);
            PlayerPrefs.Save();
        }
    }
}