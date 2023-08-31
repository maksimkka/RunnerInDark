using System;
using UnityEngine;

namespace Code.HUD.FIreIndicators
{
    public class Indicators : MonoBehaviour
    {
        [field: SerializeField] public IndicatorSettings[] IndicatorSettingsArray { get; private set; }
        
    }
}