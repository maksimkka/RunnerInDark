﻿using Code.Ground;
using Code.UnityPhysics;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Code.Hero
{
    public class GroundChecker : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<GroundData, UnityPhysicsCollisionDataComponent>> _groundCollisionFilter = default;
        private readonly EcsFilterInject<Inc<HeroData>> _heroData = default;
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _groundCollisionFilter.Value)
            {
                CheckGround(entity);
            }
        }

        private void CheckGround(int entity)
        {
            ref var collisionData = ref _groundCollisionFilter.Pools.Inc2.Get(entity);
            foreach (var collisionEnter in collisionData.CollisionsEnter)
            {
                if (collisionEnter.dto.OtherCollider.gameObject.layer == Layers.GroundChecker)
                {
                    ChangeState();
                }
            }

            collisionData.CollisionsEnter.Clear();
        }

        private void ChangeState()
        {
            foreach (var entity in _heroData.Value)
            {
                ref var heroData = ref _heroData.Pools.Inc1.Get(entity);

                heroData.IsGround = true;
            }
        }
    }
}