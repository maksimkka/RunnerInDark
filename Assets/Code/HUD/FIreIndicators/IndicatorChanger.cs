using Code.Bonus;
using Code.Water;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Code.HUD.FIreIndicators
{
    public class IndicatorChanger : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<IndicatorData>> _indicatorDataFilter = default;
        private readonly EcsFilterInject<Inc<WaterCollisionRequest>> _waterCollisionRequest = default;
        private readonly EcsFilterInject<Inc<BonusCollectRequest>> _bonusCollectRequest = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var waterCollisionEntity in _waterCollisionRequest.Value)
            {
                ref var waterCollisionRequest = ref _waterCollisionRequest.Pools.Inc1.Get(waterCollisionEntity);
                foreach (var entity in _indicatorDataFilter.Value)
                {
                    ref var indicatorData = ref _indicatorDataFilter.Pools.Inc1.Get(entity);
                    SwitchOff(ref indicatorData, waterCollisionRequest.Damage);
                }

                _waterCollisionRequest.Pools.Inc1.Del(waterCollisionEntity);
            }

            SwitchOn();
        }

        private void SwitchOn()
        {
            foreach (var bonusCollectEntity in _bonusCollectRequest.Value)
            {
                _bonusCollectRequest.Pools.Inc1.Del(bonusCollectEntity);
                foreach (var entity in _indicatorDataFilter.Value)
                {
                    ref var indicatorData = ref _indicatorDataFilter.Pools.Inc1.Get(entity);

                    if (indicatorData.IndexLastIncludedIndicator >=
                        indicatorData.Indicators.IndicatorSettingsArray.Length - 1) break;

                    indicatorData.IndexLastIncludedIndicator++;
                    indicatorData.Indicators.IndicatorSettingsArray[indicatorData.IndexLastIncludedIndicator]
                        .SwitchOn();
                }
            }
        }

        private void SwitchOff(ref IndicatorData indicatorData, int damage)
        {
            if (damage > 1)
            {
                foreach (var indicator in indicatorData.Indicators.IndicatorSettingsArray)
                {
                    indicator.SwitchOff();
                }

                ScreenSwitcher.ShowScreen(ScreenType.Defeat);
                Time.timeScale = 0;
                indicatorData.IndexLastIncludedIndicator = 0;
            }

            else
            {
                if (indicatorData.IndexLastIncludedIndicator <= 0)
                {
                    ScreenSwitcher.ShowScreen(ScreenType.Defeat);
                    Time.timeScale = 0;
                    indicatorData.Indicators.IndicatorSettingsArray[indicatorData.IndexLastIncludedIndicator]
                        .SwitchOff();
                }

                else
                {
                    indicatorData.Indicators.IndicatorSettingsArray[indicatorData.IndexLastIncludedIndicator]
                        .SwitchOff();
                }

                indicatorData.IndexLastIncludedIndicator -= damage;
            }
        }
    }
}