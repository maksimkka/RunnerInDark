using System;
using System.Collections.Generic;
using System.Threading;
using Code.Chunks;
using Code.Ground;
using Code.Hero;
using Code.HUD;
using Code.HUD.FIreIndicators;
using Code.HUD.Score;
using Code.Water;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Code.Main
{
    [DisallowMultipleComponent]
    public class LevelEntryPoint : MonoBehaviour
    {
        [SerializeField] private ScreenService _screenService;
        private readonly Dictionary<SystemType, EcsSystems> _systems = new();
        private readonly CancellationTokenSource _tokenSources = new();
        
        private EcsWorld _world;
        private EcsSystems _updateSystem;

        private void Awake()
        {
            ScreenSwitcher.Initialize(_screenService.screens);
            ScreenSwitcher.ShowScreen(ScreenType.Menu);
            Time.timeScale = 0;
        }

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
            var scoreSettings = FindObjectOfType<ScoreSettings>(true);
            var chunkGeneratorSettings = FindObjectOfType<ChunkGeneratorSettings>(true);


            foreach (var system in _systems)
            {
                system.Value.Inject(heroSettings, indicators, chunkGeneratorSettings, scoreSettings);
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
                .Add(new ChunksGeneratorInit())
                .Add(new GroundInit())
                .Add(new WaterInit())
                .Add(new HeroInit())
                .Add(new IndicatorsInit())
                .Add(new ScoreInit());

            _systems[SystemType.Update]
                .Add(new GroundChecker())
                .Add(new HeroJump())
                .Add(new WaterCollision())
                .Add(new IndicatorChanger())
                .Add(new BonusCollector())
                .Add(new ChunkGenerator())
                .Add(new ScoreCounter());
            
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