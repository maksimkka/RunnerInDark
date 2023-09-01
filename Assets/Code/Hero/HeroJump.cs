using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Code.Hero
{
    public class HeroJump : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<HeroData>> _heroFilter = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _heroFilter.Value)
            {
                ref var heroData = ref _heroFilter.Pools.Inc1.Get(entity);

                Jump(ref heroData);
            }
        }

        private void Jump(ref HeroData heroData)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (heroData.IsGround)
                {
                    heroData.HeroRigidbody.AddForce(0, heroData.JumpForce, 0, ForceMode.Impulse);
                    heroData.IsGround = false;
                }
                else
                {
                    heroData.IsGround = false;
                }
            }
        }
    }
}