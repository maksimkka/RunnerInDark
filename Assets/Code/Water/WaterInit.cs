using System.Collections.Generic;
using Code.UnityPhysics;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Code.Water
{
    public class WaterInit : IEcsInitSystem
    {
        private readonly EcsPoolInject<WaterData> _waterData = default;
        private readonly EcsCustomInject<WaterSettings[]> _waterSettings = default;
        private readonly EcsPoolInject<UnityPhysicsCollisionDataComponent> _unityPhysicsCollisionDataComponent = default;

        public void Init(IEcsSystems systems)
        {
            foreach (var waterSettings in _waterSettings.Value)
            {
                var entity = systems.GetWorld().NewEntity();
                ref var waterData = ref _waterData.Value.Add(entity);

                ref var unityPhysicsCollisionDataComponent = ref _unityPhysicsCollisionDataComponent.Value.Add(entity);
                unityPhysicsCollisionDataComponent.CollisionsEnter = new Queue<(int layer, UnityPhysicsCollisionDTO collisionDTO)>();
                waterData.Detector = waterSettings.GetComponent<UnityPhysicsCollisionDetector>();
                waterData.Detector.Init(entity, systems.GetWorld());
                waterData.Type = waterSettings.Type;
                waterData.Damage = waterSettings.Damage;
            }
        }
    }
}