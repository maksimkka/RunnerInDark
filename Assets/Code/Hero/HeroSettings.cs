using UnityEngine;

namespace Code.Hero
{
    [DisallowMultipleComponent]
    public class HeroSettings : MonoBehaviour
    {
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public float JumpForce { get; private set; }
        [field: SerializeField] public float CollectRadius { get; private set; }
        [field: SerializeField] public Light Light { get; private set; }
        [field: SerializeField] public GroundCheckerMarker GroundCheckerMarker { get; private set; }
        [field: SerializeField] public WaterCheckerMarker WaterCheckerMarker { get; private set; }
    }
}