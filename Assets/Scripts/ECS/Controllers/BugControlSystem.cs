using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using System.Linq;
using UnityEngine;

namespace Client
{
    sealed class BugControlSystem : IEcsRunSystem
    {
        readonly EcsWorldInject ecsWorld = default;
        readonly EcsFilterInject<Inc<BugController, Collisions, Target>> filter = default;
        readonly EcsPoolInject<Triggers> triggersPool = default;
        readonly EcsPoolInject<Target> targetsPool = default;
        readonly EcsPoolInject<Collisions> collisionsPool = default;
        readonly EcsPoolInject<BugController> controllerPool = default;
        readonly EcsPoolInject<ImpactDamage> impactPool = default;
        readonly EcsPoolInject<DeadTag> deadPool = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in filter.Value)
            {
                // ref var triggers = ref triggersPool.Value.Get(entity); // овтечает за область видимости и здесь не нужна 
                ref var collisions = ref collisionsPool.Value.Get(entity);
                ref var controller = ref controllerPool.Value.Get(entity);
                ref var targets = ref targetsPool.Value.Get(entity);
                if (collisions.entities.Count == 0) continue; // ни с чем не коллизируем
                foreach (var col in collisions.entities)
                {
                    if (targets.value != col) continue;
                    if (!col.Alive(ecsWorld.Value)) continue;
                    if (!impactPool.Value.Has(col.index)) continue;
                    ref var imp = ref impactPool.Value.Get(col.index);
                    imp.hits.Add(controller.damage * Time.deltaTime);
                }
            }
        }
    }
}