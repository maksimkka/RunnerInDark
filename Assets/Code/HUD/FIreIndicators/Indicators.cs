using UnityEngine;

namespace Code.HUD.FIreIndicators
{
    [DisallowMultipleComponent]
    public class Indicators : MonoBehaviour
    {
        [field: SerializeField] public IndicatorSettings[] IndicatorSettingsArray { get; private set; }
    }
}