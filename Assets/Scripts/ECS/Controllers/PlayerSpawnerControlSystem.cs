using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    sealed class PlayerSpawnerControlSystem : IEcsRunSystem
    {
        readonly EcsFilterInject<Inc<PlayerSpawnerController>, Exc<SpawnEvent>> filter = default;
        readonly EcsFilterInject<Inc<PlayerTag>> playerFilter = default;
        readonly EcsPoolInject<SpawnEvent> spawnEventsPool = default;

        public void Run(IEcsSystems systems)
        {
            if (playerFilter.Value.GetEntitiesCount() > 0) return;

            int spawnerNumber = Random.Range(0, filter.Value.GetEntitiesCount());
            int entity = filter.Value.GetRawEntities()[spawnerNumber];
            spawnEventsPool.Value.Add(entity);
        }
    }
}
