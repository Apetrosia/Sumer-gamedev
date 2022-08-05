using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using System.Linq;
using UnityEngine;

namespace Client
{
    sealed class BulletControlSystem : IEcsRunSystem
    {
        readonly EcsWorldInject ecsWorld = default;
        readonly EcsFilterInject<Inc<BulletController, Triggers>> filter = default;
        readonly EcsPoolInject<Triggers> triggersPool = default;
        readonly EcsPoolInject<BulletController> controllerPool = default;
        readonly EcsPoolInject<ImpactDamage> impactPool = default;
        readonly EcsPoolInject<DeadTag> deadPool = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in filter.Value)
            {
                ref var triggers = ref triggersPool.Value.Get(entity);
                ref var controller = ref controllerPool.Value.Get(entity);
                if (triggers.entities.Count == 0) continue;
                if (!deadPool.Value.Has(entity))
                    deadPool.Value.Add(entity); // suicide 

                var target = triggers.entities.First();
                if (!target.Alive(ecsWorld.Value)) continue;
                if (!impactPool.Value.Has(target.index)) continue;
                ref var impact = ref impactPool.Value.Get(target.index);
                impact.hits.Add(controller.damage);
            }
        }
    }
}
