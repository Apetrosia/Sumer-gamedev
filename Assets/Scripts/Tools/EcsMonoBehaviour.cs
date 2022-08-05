using System.Collections;
using System.Collections.Generic;
using Leopotam.EcsLite;
using UnityEngine;


namespace Client
{
    public class EcsMonoBehaviour : MonoBehaviour
    {
        public Entity entity { get; protected set; }
        protected EcsWorld ecsWorld;

        protected EcsPool<Triggers> triggerPool;
        protected EcsPool<Collisions> collisionPool;
        protected EcsPool<ParticleCollisions> particleCollisionPool;

        public virtual void InitEntity(EcsWorld ecsWorld)
        {
            this.ecsWorld = ecsWorld;
            triggerPool = ecsWorld.GetPool<Triggers>();
            collisionPool = ecsWorld.GetPool<Collisions>();
            
            particleCollisionPool = ecsWorld.GetPool<ParticleCollisions>();


            entity = Entity.Generate(ecsWorld);

            ref var transformComponent = ref ecsWorld.GetPool<TransformComponent>().Add(entity.index);
            transformComponent.value = transform;
        }

        public virtual void Update()
        {
            if (!entity.Alive(ecsWorld))
                Destroy(gameObject);
        }

// ############################################################################### //
// ######################## обёртки над unity событиями ########################## //
// ############################################################################### //

        protected void OnTriggerEnter2DAdapter(Collider2D other)
        {
            var otherAdapter = other.GetComponent<EcsMonoBehaviour>();
            if (otherAdapter == null) return;
            var otherEntity = otherAdapter.entity;
            if (!otherEntity.Alive(ecsWorld)) return;
            if (!entity.Alive(ecsWorld)) return;
            if (!triggerPool.Has(entity.index)) return;
            ref var triggers = ref triggerPool.Get(entity.index);
            triggers.entities.Add(otherEntity);
        }
        protected void OnCollisionEnter2DAdapter(Collision2D other)
        {
            var otherAdapter = other.gameObject.GetComponent<EcsMonoBehaviour>();
            if (otherAdapter == null) return;
            var otherEntity = otherAdapter.entity;
            if (!otherEntity.Alive(ecsWorld)) return;
            if (!entity.Alive(ecsWorld)) return;
            if (!collisionPool.Has(entity.index)) return;
            ref var collisions = ref collisionPool.Get(entity.index);
            collisions.entities.Add(otherEntity);
        }

        protected void OnTriggerExit2DAdapter(Collider2D other)
        {
            var otherAdapter = other.GetComponent<EcsMonoBehaviour>();
            if (otherAdapter == null) return;
            var otherEntity = otherAdapter.entity;
            if (!otherEntity.Alive(ecsWorld)) return;
            if (!entity.Alive(ecsWorld)) return;
            if (!triggerPool.Has(entity.index)) return;
            ref var triggers = ref triggerPool.Get(entity.index);
            if (!triggers.entities.Contains(otherEntity)) return;
            triggers.entities.Remove(otherEntity);
        }
        protected void OnCollisionExit2DAdapter(Collision2D other)
        {
            var otherAdapter = other.gameObject.GetComponent<EcsMonoBehaviour>();
            if (otherAdapter == null) return;
            var otherEntity = otherAdapter.entity; 
            if (!otherEntity.Alive(ecsWorld)) return; // проверка тот с кем сталкиваемся живы
            if (!entity.Alive(ecsWorld)) return; // проверка мы живы 
            if (!collisionPool.Has(entity.index)) return; // есть ли в пуле с кол компонент в который можно записать кол
            ref var triggers = ref collisionPool.Get(entity.index);
            if (!triggers.entities.Contains(otherEntity)) return;
            triggers.entities.Remove(otherEntity);
        }

        protected void OnParticleCollisionAdapter(GameObject other)
        {
            var otherAdapter = other.GetComponent<EcsMonoBehaviour>();
            if (otherAdapter == null) return;
            var otherEntity = otherAdapter.entity;
            if (!otherEntity.Alive(ecsWorld)) return;
            if (!entity.Alive(ecsWorld)) return;
            if (!particleCollisionPool.Has(entity.index)) return;
            ref var collisions = ref particleCollisionPool.Get(entity.index);
            collisions.entities.Add(otherEntity);
        }
    }
}