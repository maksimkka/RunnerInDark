using System.Collections.Generic;
using UnityEngine;

namespace Code.Chunks
{
    [DisallowMultipleComponent]
    public class ChunkGeneratorSettings : MonoBehaviour
    {
        [field: SerializeField] public List<ChunkSettings> ChunkSettings { get; private set; }
        [field: SerializeField] public ChunkSettings FirstChunk { get; private set; }
        [field: SerializeField] public int StartPoolSize { get; private set; }
    }
}