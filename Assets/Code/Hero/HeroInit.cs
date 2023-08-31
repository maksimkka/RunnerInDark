using System.Collections.Generic;
using Code.UnityPhysics;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Code.Hero
{
    public class HeroInit : IEcsInitSystem
    {
        private readonly EcsPoolInject<HeroData> _heroData = default;
        private readonly EcsPoolInject<GroundCheckerData> _groundChecker = default;
        private readonly EcsPoolInject<WaterCheckerData> _waterCheckerMarker = default;
        private readonly EcsPoolInject<UnityPhysicsCollisionDataComponent> _unityPhysicsCollisionDataComponent = default;

        private readonly EcsCustomInject<HeroSettings> _heroSettings = default;
        
        public void Init(IEcsSystems systems)
        {
            var entity = systems.GetWorld().NewEntity();
            ref var heroData = ref _heroData.Value.Add(entity);

            ref var currentGroundData = ref _groundChecker.Value.Add(entity);
            currentGroundData.Detector = _heroSettings.Value.GroundCheckerMarker.GetComponent<UnityPhysicsCollisionDetector>();
            currentGroundData.Detector.Init(entity, systems.GetWorld());

            ref var waterCheckerMarker = ref _waterCheckerMarker.Value.Add(entity);
            waterCheckerMarker.Detector = _heroSettings.Value.WaterCheckerMarker.GetComponent<UnityPhysicsCollisionDetector>();
            waterCheckerMarker.Detector.Init(entity, systems.GetWorld());
            
            ref var c_collisionData = ref _unityPhysicsCollisionDataComponent.Value.Add(entity);
            c_collisionData.CollisionsEnter = new Queue<(int layer, UnityPhysicsCollisionDTO collisionDTO)>();
            
            var heroGameObject = _heroSettings.Value.gameObject;
            heroData.HeroGameObject = heroGameObject;
            heroData.HeroRigidbody = heroGameObject.GetComponent<Rigidbody>();
            heroData.Speed = _heroSettings.Value.Speed;
            heroData.JumpForce = _heroSettings.Value.JumpForce;
            heroData.CollectRadius = _heroSettings.Value.CollectRadius;
            heroData.Light = _heroSettings.Value.Light;
        }
    }
}