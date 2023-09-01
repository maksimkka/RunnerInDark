using System.Collections.Generic;
using Code.UnityPhysics;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Code.Water
{
    public class WaterInit : IEcsInitSystem
    {
        private readonly EcsFilterInject<Inc<WaterData>> _waterData = default;
        private readonly EcsPoolInject<UnityPhysicsCollisionDataComponent> _unityPhysicsCollisionDataComponent = default;

        public void Init(IEcsSystems systems)
        {
            foreach (var entity in _waterData.Value)
            {
                ref var waterData = ref _waterData.Pools.Inc1.Get(entity);

                ref var unityPhysicsCollisionDataComponent = ref _unityPhysicsCollisionDataComponent.Value.Add(entity);
                unityPhysicsCollisionDataComponent.CollisionsEnter = new Queue<(int layer, UnityPhysicsCollisionDTO collisionDTO)>();
                waterData.Detector = waterData.WaterSettings.GetComponent<UnityPhysicsCollisionDetector>();
                waterData.Detector.Init(entity, systems.GetWorld());
                waterData.Damage = waterData.WaterSettings.Damage;
            }
        }
    }
}