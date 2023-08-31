using Code.Logger;
using Code.UnityPhysics;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Code.Water
{
    public class WaterCollision : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<WaterData, UnityPhysicsCollisionDataComponent>> _waterCollisionFilter = default;
        private readonly EcsPoolInject<WaterCollisionRequest> _waterCollisionRequest = default;
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _waterCollisionFilter.Value)
            {
                CollisionHandler(entity);
            }
        }

        private void CollisionHandler(int entity)
        {
            ref var collisionData = ref _waterCollisionFilter.Pools.Inc2.Get(entity);

            foreach (var collision in collisionData.CollisionsEnter)
            {
                if (collision.dto.OtherCollider.gameObject.layer == Layers.WaterChecker)
                {
                    var waterEntity = collision.dto.SelfEntity;
                    ref var waterData = ref _waterCollisionFilter.Pools.Inc1.Get(waterEntity);

                    if (!_waterCollisionRequest.Value.Has(entity))
                    {
                        ref var waterCollisionRequest = ref _waterCollisionRequest.Value.Add(entity);
                        waterCollisionRequest.Damage = waterData.Damage;
                    }

                    //DefaultWaterCollision(ref waterData);

                    // if (waterData.Type == WaterType.DefaultWater)
                    // {
                    // }
                    //
                    // else if(waterData.Type == WaterType.DeadlyWater)
                    // {
                    //     DeadlyWaterCollision(ref waterData);
                    // }
                }
            }
            
            collisionData.CollisionsEnter.Clear();
        }

        private void DefaultWaterCollision(ref WaterData waterData)
        {
            $"STOLKNULSYA".Colored(Color.cyan).Log();
        }
        
        private void DeadlyWaterCollision(ref WaterData waterData)
        {
            $"STOLKNULSYA".Colored(Color.cyan).Log();
        }
    }
}