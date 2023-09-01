using System.Linq;
using Code.Hero;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Code.Chunks
{
    public class ChunkGenerator : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<ChunkGeneratorData>> _chunkGeneratorFilter = default;
        private readonly EcsFilterInject<Inc<HeroData>> _heroDataFilter = default;

        private Vector3 _heroPosition;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _chunkGeneratorFilter.Value)
            {
                ref var chunkGenerator = ref _chunkGeneratorFilter.Pools.Inc1.Get(entity);

                CheckDistance(ref chunkGenerator);
            }
        }

        private void CheckDistance(ref ChunkGeneratorData chunkGenerator)
        {
            foreach (var entity in _heroDataFilter.Value)
            {
                ref var heroData = ref _heroDataFilter.Pools.Inc1.Get(entity);
                _heroPosition = heroData.HeroGameObject.transform.position;

                if (_heroPosition.z > chunkGenerator.SpawnedChunksList[^1].Begin.position.z)
                {
                    SpawnChunk(ref chunkGenerator);
                }
            }
        }

        private void SpawnChunk(ref ChunkGeneratorData chunkGenerator)
        {
            var inactiveObjects = chunkGenerator.PoolChunksList.Where(obj =>
                !obj.gameObject.activeSelf).ToList();

            int randomIndex = Random.Range(0, inactiveObjects.Count);

            var newChunk = inactiveObjects[randomIndex];
            newChunk.gameObject.SetActive(true);
            EnableBonuses(newChunk.gameObject);

            newChunk.transform.position =
                chunkGenerator.SpawnedChunksList[^1].End.position - newChunk.Begin.localPosition;

            chunkGenerator.SpawnedChunksList.Add(newChunk);

            if (chunkGenerator.SpawnedChunksList.Count > 3)
            {
                chunkGenerator.SpawnedChunksList[0].gameObject.SetActive(false);
                chunkGenerator.SpawnedChunksList.RemoveAt(0);
            }
        }

        private void EnableBonuses(GameObject chunk)
        {
            var inactiveChildren = chunk.transform.Cast<Transform>().Where(child => !child.gameObject.activeSelf);
            foreach (var childTransform in inactiveChildren)
            {
                childTransform.gameObject.SetActive(true);
            }
        }
    }
}