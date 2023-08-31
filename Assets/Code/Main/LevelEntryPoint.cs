using System;
using System.Collections.Generic;
using System.Threading;
using Code.Bonus;
using Code.Ground;
using Code.Hero;
using Code.HUD.FIreIndicators;
using Code.Water;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Code.Main
{
    [DisallowMultipleComponent]
    public class LevelEntryPoint : MonoBehaviour
    {
        private readonly Dictionary<SystemType, EcsSystems> _systems = new();
        private readonly CancellationTokenSource _tokenSources = new();
        
        private EcsWorld _world;
        private EcsSystems _updateSystem;

        private void Start()
        {
            InitECS();
        }

        private void DistributeDataBetweenGameModes()
        {
            AddGameSystems();
            InjectGameObjects();
        }

        private void InitECS()
        {
            _world = new EcsWorld();
            var systemTypes = Enum.GetValues(typeof(SystemType));
            foreach (var item in systemTypes)
            {
                _systems.Add((SystemType)item, new EcsSystems(_world));
            }

#if UNITY_EDITOR
            AddDebugSystems();
#endif

            DistributeDataBetweenGameModes();

            foreach (var system in _systems)
            {
                system.Value.Init();
            }
        }

        private void InjectGameObjects()
        {
            var heroSettings = FindObjectOfType<HeroSettings>(true);
            var indicators = FindObjectOfType<Indicators>(true);
            var groundMarkers = FindObjectsOfType<GroundMarker>(true);
            var waterSettings = FindObjectsOfType<WaterSettings>(true);
            var bonusMarkers = FindObjectsOfType<BonusMarker>(true);


            foreach (var system in _systems)
            {
                system.Value.Inject(heroSettings, groundMarkers, waterSettings, indicators, bonusMarkers);
            }
        }

        private void Update()
        {
            _systems[SystemType.Update].Run();
        }

        private void FixedUpdate()
        {
            _systems[SystemType.FixedUpdate].Run();
        }

        private void AddDebugSystems()
        {
#if UNITY_EDITOR
            _systems[SystemType.Update].Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem());
#endif
        }

        private void AddGameSystems()
        {
            _systems[SystemType.Init]
                .Add(new HeroInit())
                .Add(new GroundInit())
                .Add(new WaterInit())
                .Add(new IndicatorsInit())
                .Add(new BonusInit());

            _systems[SystemType.Update]
                .Add(new GroundChecker())
                .Add(new HeroJump())
                .Add(new WaterCollision())
                .Add(new IndicatorChanger())
                .Add(new BonusCollector());
            
            _systems[SystemType.FixedUpdate]
                .Add(new HeroMove());

        }

        private void OnDestroy()
        {
            _world?.Destroy();
            foreach (var system in _systems)
            {
                system.Value.Destroy();
            }

            _tokenSources.Cancel();
            _tokenSources.Dispose();
        }
    }
}