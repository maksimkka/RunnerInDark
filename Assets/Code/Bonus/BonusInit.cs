using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Code.Bonus
{
    public class BonusInit : IEcsInitSystem
    {
        private readonly EcsPoolInject<BonusData> _bonusData = default;
        private readonly EcsCustomInject<BonusMarker[]> _bonusesMarker = default;

        public void Init(IEcsSystems systems)
        {
            foreach (var bonusMarker in _bonusesMarker.Value)
            {
                var entity = systems.GetWorld().NewEntity();
                ref var bonusData = ref _bonusData.Value.Add(entity);
                bonusData.BonusGameObject = bonusMarker.gameObject;
            }
        }
    }
}