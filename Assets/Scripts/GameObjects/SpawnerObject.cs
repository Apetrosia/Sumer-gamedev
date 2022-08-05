using System.Collections;
using System.Collections.Generic;
using Leopotam.EcsLite;
using UnityEngine;


namespace Client
{
    public class SpawnerObject : EcsMonoBehaviour
    {
        public float hp = 10;
        public float spawnRadius = 1;
        public float period = 1;
        public GameObject spawnObject;


        public override void InitEntity(EcsWorld ecsWorld)
        {
            base.InitEntity(ecsWorld);

            ref var triggers = ref ecsWorld.GetPool<Triggers>().Add(entity.index);
            ref var health = ref ecsWorld.GetPool<Health>().Add(entity.index);
            ref var impactDamage = ref ecsWorld.GetPool<ImpactDamage>().Add(entity.index);
            ref var spawner = ref ecsWorld.GetPool<Spawner>().Add(entity.index);
            ref var timer = ref ecsWorld.GetPool<PeriodicTimer>().Add(entity.index);
            ecsWorld.GetPool<TargetingAtPlayer>().Add(entity.index);
            ecsWorld.GetPool<SpawnerObjectController>().Add(entity.index);

            triggers.entities = new HashSet<Entity>();
            health.maxValue = hp;
            health.value = hp;
            impactDamage.hits = new List<float>();
            spawner.target = spawnObject;
            spawner.radius = spawnRadius;
            timer.period = period;
        }

        private void OnTriggerEnter2D(Collider2D other) =>
            OnTriggerEnter2DAdapter(other);

        private void OnTriggerExit2D(Collider2D other) =>
            OnTriggerExit2DAdapter(other);
    }
}
