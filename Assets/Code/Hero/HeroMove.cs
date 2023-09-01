using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Code.Hero
{
    public class HeroMove : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<HeroData>> _heroFilter = default;

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
            heroData.HeroGameObject.transform.Translate(new Vector3(0, 0, heroData.Speed * Time.fixedDeltaTime));
        }
    }
}