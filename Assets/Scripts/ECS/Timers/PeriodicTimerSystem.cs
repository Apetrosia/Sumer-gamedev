using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    sealed class PeriodicTimerSystem: IEcsRunSystem
    {
        readonly EcsFilterInject<Inc<PeriodicTimer>> filter = default;

        readonly EcsPoolInject<PeriodicTimer> timerPool = default;
        readonly EcsPoolInject<TimerFinishEvent> timerEventPool = default;


        public void Run(IEcsSystems systems)
        {
            foreach (var entity in filter.Value)
            {
                ref var timer = ref timerPool.Value.Get(entity);
                timer.timeToFinish -= Time.deltaTime;
                if (timer.timeToFinish <= 0)
                {
                    timer.timeToFinish = timer.period;
                    if (timerEventPool.Value.Has(entity)) continue;
                    timerEventPool.Value.Add(entity);
                }
            }
        }
    }
}
