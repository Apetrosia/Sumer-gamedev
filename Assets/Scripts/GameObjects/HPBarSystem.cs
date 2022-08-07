using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using UnityEngine.UI;

namespace Client
{
    public class HPBarSystem : IEcsRunSystem
    {
        public Text HPtext;
        readonly EcsWorldInject ecsWorld = default;
        readonly EcsFilterInject<Inc<HPBarComponent>> filter = default;
        readonly EcsFilterInject<Inc<PlayerTag>> playerFilter = default;
        readonly EcsPoolInject<Damaged> damagedPool = default;
        readonly EcsPoolInject<HPBarComponent> HPBarPool = default;
        //readonly EcsPoolInject<TransformComponent> transformPool = default;
        //readonly EcsPoolInject<ImpactDamage> damagePool = default; // max = 10

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in filter.Value)
            {
                ref var playerDamaged = ref damagedPool.Value.Get(playerFilter.Value.GetRawEntities()[0]);
                ref var hpcomponent = ref HPBarPool.Value.Get(entity);
                hpcomponent.HPtext.text = $"Player Health: {System.Math.Round(playerDamaged.currentHealth)}";
                continue;
            }
        }
    }
}