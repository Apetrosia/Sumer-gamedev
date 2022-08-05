using System.Collections;
using System.Collections.Generic;
using Leopotam.EcsLite;
using UnityEngine;


namespace Client
{
    [RequireComponent(typeof(ParticleSystem))]
    public class Flamethrower: EcsMonoBehaviour
    {
        public float force;
        public override void InitEntity(EcsWorld ecsWorld)
        {
            base.InitEntity(ecsWorld);

            ref var particles = ref ecsWorld.GetPool<ParticlesComponent>().Add(entity.index);
            ref var collisions = ref ecsWorld.GetPool<ParticleCollisions>().Add(entity.index);
            ref var damageParticle = ref ecsWorld.GetPool<DamageParticleCollided>().Add(entity.index);
            ecsWorld.GetPool<RotationLikeParent>().Add(entity.index);
            ecsWorld.GetPool<PositionLikeParent>().Add(entity.index);
            ecsWorld.GetPool<DeadLikeParent>().Add(entity.index);
            ecsWorld.GetPool<ParticlesPlayIfFire>().Add(entity.index);
            ecsWorld.GetPool<FireAutoClear>().Add(entity.index);

            particles.value = GetComponent<ParticleSystem>();
            collisions.entities = new List<Entity>();
            damageParticle.force = force;
        }

        void OnParticleCollision(GameObject other) =>
            OnParticleCollisionAdapter(other);
    }
}
