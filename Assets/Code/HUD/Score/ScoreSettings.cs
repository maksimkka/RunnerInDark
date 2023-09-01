using TMPro;
using UnityEngine;

namespace Code.HUD.Score
{
    [DisallowMultipleComponent]
    public class ScoreSettings : MonoBehaviour
    {
        [field: SerializeField] public TextMeshProUGUI BestScoreText { get; private set; }
        [field: SerializeField] public TextMeshProUGUI ScoreTextGameScreen { get; private set; }
        [field: SerializeField] public TextMeshProUGUI ScoreTextDefeatScreenScreen { get; private set; }
    }
}