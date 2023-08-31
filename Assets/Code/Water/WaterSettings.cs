using UnityEngine;

namespace Code.Water
{
    public class WaterSettings : MonoBehaviour
    {
        [field: SerializeField] public WaterType Type { get; private set; }
        [field: SerializeField] public int Damage { get; private set; }
    }
}