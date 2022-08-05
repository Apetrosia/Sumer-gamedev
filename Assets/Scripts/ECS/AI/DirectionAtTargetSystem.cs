using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    sealed class DirectionAtTargetSystem : IEcsRunSystem
    {
        readonly EcsWorldInject ecsWorld = default;
        readonly EcsFilterInject<Inc<DirectionAtTarget, Target, TransformComponent>> filter = default;

        readonly EcsPoolInject<Target> targetPool = default;
        readonly EcsPoolInject<Direction> directionPool = default;
        readonly EcsPoolInject<TransformComponent> transformPool = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in filter.Value)
            {
                ref var target = ref targetPool.Value.Get(entity);
                if (!target.value.Alive(ecsWorld.Value)) continue;
                if (!transformPool.Value.Has(target.value.index)) continue;
                var targetPos = (Vector2)transformPool.Value.Get(target.value.index).value.position;
                var pos = (Vector2)transformPool.Value.Get(entity).value.position;

                var delta = targetPos - pos;
                delta.Normalize();
                if (!directionPool.Value.Has(entity))
                    directionPool.Value.Add(entity);
                ref var direction = ref directionPool.Value.Get(entity);
                direction.value = delta;
            }
        }
    }
}
