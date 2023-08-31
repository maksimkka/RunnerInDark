using Code.Bonus;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Code.Hero
{
    public class BonusCollector : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<HeroData>> _heroDataFilter = default;
        private readonly EcsFilterInject<Inc<BonusData>> _bonusDataFilter = default;
        private readonly EcsPoolInject<BonusCollectRequest> _bonusCollectRequest = default;

        private EcsWorld _world;
        public void Run(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            
            foreach (var entity in _bonusDataFilter.Value)
            {
                ref var _bonusData = ref _bonusDataFilter.Pools.Inc1.Get(entity);
                if(!_bonusData.BonusGameObject.activeSelf) continue;
                CheckDistance(_bonusData.BonusGameObject.transform);
            }
        }

        private void CheckDistance(Transform bonusTransform)
        {
            foreach (var entity in _heroDataFilter.Value)
            {
                ref var heroData = ref _heroDataFilter.Pools.Inc1.Get(entity);

                var distance = Vector3.Distance(heroData.HeroGameObject.transform.position, bonusTransform.position);

                if (distance <= heroData.CollectRadius)
                {
                    CollectBonus(bonusTransform);
                }
            }
        }
        
        private void CollectBonus(Transform bonusTransform)
        {
            var newEntity = _world.NewEntity();
            _bonusCollectRequest.Value.Add(newEntity);
            bonusTransform.gameObject.SetActive(false);
        }
    }
}