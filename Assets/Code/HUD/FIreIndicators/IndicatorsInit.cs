using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Code.HUD.FIreIndicators
{
    public class IndicatorsInit : IEcsInitSystem
    {
        private readonly EcsPoolInject<IndicatorData> _indicatorData = default;
        private readonly EcsCustomInject<Indicators> _indicators = default;

        public void Init(IEcsSystems systems)
        {
            var entity = systems.GetWorld().NewEntity();
            ref var indicatorData = ref _indicatorData.Value.Add(entity);

            indicatorData.Indicators = _indicators.Value;
            indicatorData.IndexLastIncludedIndicator = indicatorData.Indicators.IndicatorSettingsArray.Length - 1;
        }
    }
}