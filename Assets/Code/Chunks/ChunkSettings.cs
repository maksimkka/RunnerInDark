using UnityEngine;

namespace Code.Chunks
{
    [DisallowMultipleComponent]
    public class ChunkSettings : MonoBehaviour
    {
        [field: SerializeField] public Transform Begin { get; private set; }
        [field: SerializeField] public Transform End { get; private set; }
    }
}