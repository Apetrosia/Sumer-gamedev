using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    
    sealed class KeepDistanceSystem : IEcsRunSystem
    {
        public float minDistance = 2f;

        readonly EcsWorldInject ecsWorld = default;
        readonly EcsFilterInject<Inc<TargetingAtPlayer, Target, KeepDistance>> filter = default;

        readonly EcsPoolInject<Target> targetPool = default;
        readonly EcsPoolInject<TransformComponent> transformPool = default;
        readonly EcsPoolInject<Speed> speedPool = default;


        public void Run(IEcsSystems systems)
        {
            foreach (var entity in filter.Value)
            {
                ref var target = ref targetPool.Value.Get(entity);
                if (!target.value.Alive(ecsWorld.Value)) continue;
                if (!transformPool.Value.Has(target.value.index)) continue;

                var targetPos = (Vector2)transformPool.Value.Get(target.value.index).value.position;
                var pos = (Vector2)transformPool.Value.Get(entity).value.position;

                var disttotarget = Vector2.Distance(targetPos,pos);

                if (!speedPool.Value.Has(entity))
                    speedPool.Value.Add(entity);
                ref var speed = ref speedPool.Value.Get(entity);

                if (disttotarget >= minDistance)
                {
                    speed.value = 1;
                }else{
                    speed.value = 0;
                }
                
                
            }
        }
    }
}
