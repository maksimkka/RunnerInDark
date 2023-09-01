using System.Collections.Generic;
using Code.UnityPhysics;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Code.Ground
{
    public class GroundInit : IEcsInitSystem
    {
        private readonly EcsFilterInject<Inc<GroundData>> _groundData = default;

        private readonly EcsPoolInject<UnityPhysicsCollisionDataComponent>
            _unityPhysicsCollisionDataComponent = default;

        public void Init(IEcsSystems systems)
        {
            foreach (var entity in _groundData.Value)
            {
                ref var groundData = ref _groundData.Pools.Inc1.Get(entity);
                ref var unityPhysicsCollisionDataComponent =
                    ref _unityPhysicsCollisionDataComponent.Value.Add(entity);
                unityPhysicsCollisionDataComponent.CollisionsEnter =
                    new Queue<(int layer, UnityPhysicsCollisionDTO collisionDTO)>();
                groundData.Detector = groundData.GroundMarker.GetComponent<UnityPhysicsCollisionDetector>();
                groundData.Detector.Init(entity, systems.GetWorld());
            }
        }
    }
}