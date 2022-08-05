using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    sealed class DeadLikeParentSystem : IEcsRunSystem
    {
        readonly EcsWorldInject ecsWorld = default;
        readonly EcsFilterInject<Inc<Parent>, Exc<DeadTag>> filter = default;

        readonly EcsPoolInject<Parent> parentPool = default;
        readonly EcsPoolInject<DeadTag> deadPool = default;

        public void Run(IEcsSystems systems)
        {
            foreach(var entity in filter.Value)
            {
                ref var parent = ref parentPool.Value.Get(entity);
                if (!parent.value.Alive(ecsWorld.Value)
                    || deadPool.Value.Has(parent.value.index))
                    deadPool.Value.Add(entity);
            }
        }
    }
}
