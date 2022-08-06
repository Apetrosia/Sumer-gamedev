using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    public class HPBarSystem : IEcsRunSystem
    {
        readonly EcsWorldInject ecsWorld = default;
        readonly EcsFilterInject<HPBarComponent> filter = default;
        readonly EcsFilterInject<Inc<Player>> playerFilter = default;
        readonly EcsPoolInject<TransformComponent> transformPool = default;
        readonly EcsPoolInject<ImpactDamage> damagePool = default; // max = 10

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in filter.value)
            {
                foreach (var player in playerFilter)
                {
                    if (!player.Value.Alive) continue;
                }
            }
        }
    }
}