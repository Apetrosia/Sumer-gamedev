using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    sealed class SpawnerObjectControlSystem : IEcsRunSystem
    {
        readonly EcsFilterInject<Inc<SpawnerObjectController, Target, TimerFinishEvent>, Exc<SpawnEvent>> filter = default;
        readonly EcsPoolInject<TimerFinishEvent> timerEventsPool = default;
        readonly EcsPoolInject<SpawnEvent> spawnEventsPool = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in filter.Value)
            {
                spawnEventsPool.Value.Add(entity);
                timerEventsPool.Value.Del(entity);
            }
        }
    }
}
