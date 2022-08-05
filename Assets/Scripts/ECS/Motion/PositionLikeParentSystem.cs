using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    sealed class PositionLikeParentSystem : IEcsRunSystem
    {
        readonly EcsWorldInject ecsWorld = default;
        readonly EcsFilterInject<Inc<PositionLikeParent, TransformComponent, Parent>> filter = default;

        readonly EcsPoolInject<Parent> parentPool = default;
        readonly EcsPoolInject<TransformComponent> transformPool = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in filter.Value)
            {
                ref var parent = ref parentPool.Value.Get(entity);
                ref var transform = ref transformPool.Value.Get(entity);

                if (!parent.value.Alive(ecsWorld.Value)) continue;
                if (!transformPool.Value.Has(parent.value.index)) continue;
                var parentPosition = transformPool.Value.Get(parent.value.index).value.position;

                transform.value.position = parentPosition;
            }
        }
    }
}
