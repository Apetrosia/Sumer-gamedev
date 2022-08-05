using System.Collections;
using System.Collections.Generic;
using Leopotam.EcsLite;
using UnityEngine;


namespace Client
{
    public class StaticShooter : EcsMonoBehaviour
    {
        public float hp = 10;
        public GameObject weapon;

        public override void InitEntity(EcsWorld ecsWorld)
        {
            base.InitEntity(ecsWorld);

            ref var triggers = ref ecsWorld.GetPool<Triggers>().Add(entity.index);
            ref var health = ref ecsWorld.GetPool<Health>().Add(entity.index);
            ref var impactDamage = ref ecsWorld.GetPool<ImpactDamage>().Add(entity.index);
            ref var createWeapon = ref ecsWorld.GetPool<CreateWeapon>().Add(entity.index);
            ecsWorld.GetPool<TargetingAtPlayer>().Add(entity.index);
            ecsWorld.GetPool<DirectionAtTarget>().Add(entity.index);
            ecsWorld.GetPool<RotateToDirection>().Add(entity.index);
            ecsWorld.GetPool<FireIfTarget>().Add(entity.index);

            triggers.entities = new HashSet<Entity>();
            health.maxValue = hp;
            health.value = hp;
            impactDamage.hits = new List<float>();
            createWeapon.weapon = weapon;
        }

        private void OnTriggerEnter2D(Collider2D other) =>
            OnTriggerEnter2DAdapter(other);

        private void OnTriggerExit2D(Collider2D other) =>
            OnTriggerExit2DAdapter(other);

    }
}
