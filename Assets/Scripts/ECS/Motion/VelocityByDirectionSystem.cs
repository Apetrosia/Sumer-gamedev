using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    sealed class VelocityByDirectionSystem : IEcsRunSystem
    {
        readonly EcsFilterInject<Inc<VelocityByDirection, BodyComponent, Direction, Speed>> filter = default;

        readonly EcsPoolInject<BodyComponent> bodyPool = default;
        readonly EcsPoolInject<Direction> directionPool = default;
        readonly EcsPoolInject<Speed> speedPool = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in filter.Value)
            {
                ref var body = ref bodyPool.Value.Get(entity);
                ref var direction = ref directionPool.Value.Get(entity);
                ref var speed = ref speedPool.Value.Get(entity);
                body.value.velocity = direction.value * speed.value;
            }
        }
    }
}