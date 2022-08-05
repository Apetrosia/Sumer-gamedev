using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    sealed class SpawnIfTargetSystem : IEcsRunSystem
    {
        readonly EcsFilterInject<Inc<SpawnIfTarget, Target>, Exc<SpawnEvent>> filter = default;

        readonly EcsPoolInject<SpawnEvent> spawnEventPool = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in filter.Value)
            {
                spawnEventPool.Value.Add(entity);
            }
        }
    }
}
