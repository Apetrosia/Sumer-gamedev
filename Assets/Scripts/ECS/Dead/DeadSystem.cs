using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    sealed class DeadSystem : IEcsRunSystem
    {
        readonly EcsWorldInject ecsWorld = default;
        readonly EcsFilterInject<Inc<DeadTag>> filter = default;

        public void Run(IEcsSystems systems)
        {
            foreach(var entity in filter.Value)
            {
                ecsWorld.Value.DelEntity(entity);
            }
        }
    }
}
