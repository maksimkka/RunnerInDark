using Code.Logger;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Code.Hero
{
    public class HeroJump : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<HeroData>> _heroFilter;
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _heroFilter.Value)
            {
                ref var heroData = ref _heroFilter.Pools.Inc1.Get(entity);

                Jump(ref heroData);
                //Jump(ref heroData);
            }
        }
        private void Jump(ref HeroData heroData)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //$"11111111".Colored(Color.cyan).Log();
                
                if (heroData.IsGround)
                {
                    heroData.HeroRigidbody.AddForce(0, heroData.JumpForce, 0, ForceMode.Impulse);
                    heroData.IsGround = false;
                    //$"222222222".Colored(Color.cyan).Log();
                }
                else
                {
                    heroData.IsGround = false;
                    //$"333333333".Colored(Color.cyan).Log();
                }
            }
        }
    }
}