using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    sealed class RotateToDirectionSystem : IEcsRunSystem
    {
        readonly EcsFilterInject<Inc<RotateToDirection, Direction>> filter = default;

        readonly EcsPoolInject<TransformComponent> transformPool = default;
        readonly EcsPoolInject<Direction> directionPool = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in filter.Value)
            {
                ref var transform = ref transformPool.Value.Get(entity);
                ref var direction = ref directionPool.Value.Get(entity);
                var d = direction.value;
                transform.value.rotation = Quaternion.AngleAxis(
                    -Mathf.Acos(d.y) * Mathf.Sign(d.x) * 180 / Mathf.PI,
                    new Vector3(0, 0, 1));
            }
        }
    }
}
