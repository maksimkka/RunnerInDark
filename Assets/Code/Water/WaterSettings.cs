using UnityEngine;

namespace Code.Water
{
    [DisallowMultipleComponent]
    public class WaterSettings : MonoBehaviour
    {
        [field: SerializeField] public int Damage { get; private set; }
    }
}