using System.Collections.Generic;
using Code.Bonus;
using Code.Ground;
using Code.Water;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Code.Chunks
{
    public class ChunksGeneratorInit : IEcsInitSystem
    {
        private readonly EcsPoolInject<ChunkGeneratorData> _chunkData = default;
        private readonly EcsPoolInject<GroundData> _groundData = default;
        private readonly EcsPoolInject<WaterData> _waterData = default;
        private readonly EcsPoolInject<BonusData> _bonusData = default;
        private readonly EcsCustomInject<ChunkGeneratorSettings> _chunkSettings = default;
        private EcsWorld _ecsWorld;

        public void Init(IEcsSystems systems)
        {
            _ecsWorld = systems.GetWorld();
            var entity = systems.GetWorld().NewEntity();
            ref var chunkData = ref _chunkData.Value.Add(entity);


            chunkData.ChunksList = new List<ChunkSettings>();
            chunkData.ChunksList = _chunkSettings.Value.ChunkSettings;
            chunkData.SpawnedChunksList = new List<ChunkSettings>();
            chunkData.SpawnedChunksList.Add(_chunkSettings.Value.FirstChunk);
            chunkData.PoolChunksList = new List<ChunkSettings>();

            var startPoolSize = _chunkSettings.Value.StartPoolSize;

            for (int i = 0; i < startPoolSize; i++)
            {
                var randomIndex = Random.Range(0, chunkData.ChunksList.Count);
                var newChunk = Object.Instantiate(chunkData.ChunksList[randomIndex]);
                newChunk.transform.position = new Vector3(15, 15, 15);
                newChunk.gameObject.SetActive(false);
                chunkData.PoolChunksList.Add(newChunk);
            }

            chunkData.PoolChunksList.Add(_chunkSettings.Value.FirstChunk);
            BonusInit();
            GroundInit();
            WaterInit();
        }

        private void GroundInit()
        {
            var groundMarkers = Object.FindObjectsOfType<GroundMarker>(true);

            foreach (var groundMarker in groundMarkers)
            {
                var gridEntity = _ecsWorld.NewEntity();
                ref var groundData = ref _groundData.Value.Add(gridEntity);
                groundData.GroundMarker = groundMarker;
            }
        }

        private void WaterInit()
        {
            var watersSettings = Object.FindObjectsOfType<WaterSettings>(true);

            foreach (var waterSetting in watersSettings)
            {
                var waterEntity = _ecsWorld.NewEntity();
                ref var waterData = ref _waterData.Value.Add(waterEntity);
                waterData.WaterSettings = waterSetting;
            }
        }

        private void BonusInit()
        {
            var bonusMarkers = Object.FindObjectsOfType<BonusMarker>(true);
            foreach (var bonusMarker in bonusMarkers)
            {
                var bonusEntity = _ecsWorld.NewEntity();
                ref var bonusData = ref _bonusData.Value.Add(bonusEntity);
                bonusData.BonusMarker = bonusMarker;
            }
        }
    }
}