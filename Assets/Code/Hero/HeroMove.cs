using Code.Logger;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Code.Hero
{
    public class HeroMove : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<HeroData>> _heroFilter;
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _heroFilter.Value)
            {
                ref var heroData = ref _heroFilter.Pools.Inc1.Get(entity);

                Move(ref heroData);
            }
        }

        private void Move(ref HeroData heroData)
        {
            heroData.HeroRigidbody.velocity = new Vector3(0, heroData.HeroRigidbody.velocity.y, heroData.Speed * Time.deltaTime);
        }
    }
}