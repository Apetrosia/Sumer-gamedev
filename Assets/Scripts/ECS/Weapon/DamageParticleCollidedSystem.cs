using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    sealed class DamageParticleCollidedSystem : IEcsRunSystem
    {
        readonly EcsWorldInject ecsWorld = default;
        readonly EcsFilterInject<Inc<DamageParticleCollided, ParticleCollisions>> filter = default;
        readonly EcsPoolInject<DamageParticleCollided> damagePool = default;
        readonly EcsPoolInject<ParticleCollisions> collisionPool = default;
        readonly EcsPoolInject<ImpactDamage> impactPool = default;

        public void Run(IEcsSystems systems)
        {
            foreach(var entity in filter.Value)
            {
                ref var damage = ref damagePool.Value.Get(entity);
                ref var collision = ref collisionPool.Value.Get(entity);

                foreach (var collided in collision.entities)
                {
                    if (!collided.Alive(ecsWorld.Value)) continue;
                    if (!impactPool.Value.Has(collided.index)) continue;
                    ref var impact = ref impactPool.Value.Get(collided.index);
                    impact.hits.Add(damage.force);
                }
                collision.entities.Clear();
            }
        }
    }
}
