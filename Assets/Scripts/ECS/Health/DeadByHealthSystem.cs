using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    sealed class DeadByHealthSystem : IEcsRunSystem
    {
        readonly EcsFilterInject<Inc<Health>, Exc<DeadTag>> filter = default;
        readonly EcsPoolInject<Health> healthPool = default;
        readonly EcsPoolInject<DeadTag> deadPool = default;

        public void Run(IEcsSystems systems)
        {
            foreach(var entity in filter.Value)
            {
                ref var health = ref healthPool.Value.Get(entity);
                if (health.value > 0) continue;
                deadPool.Value.Add(entity);
            }
        }
    }
}
