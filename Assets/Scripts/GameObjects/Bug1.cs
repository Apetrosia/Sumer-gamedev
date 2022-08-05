using System;
using System.Collections;
using System.Collections.Generic;
using Leopotam.EcsLite;
using UnityEngine;


namespace Client
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Animator))]
    public class Bug1 : EcsMonoBehaviour
    {
        public float damage = 1;
        public float hp = 10;
        public float speed = 5;
        public GameObject DeadBlob;

        public override void InitEntity(EcsWorld ecsWorld)
        {
            base.InitEntity(ecsWorld);

            ref var body = ref ecsWorld.GetPool<BodyComponent>().Add(entity.index);
            ref var speed = ref ecsWorld.GetPool<Speed>().Add(entity.index);
            ref var animator = ref ecsWorld.GetPool<AnimatorComponent>().Add(entity.index);
            ref var animationSpeed = ref ecsWorld.GetPool<AnimationSpeedByVelocity>().Add(entity.index);
            ref var triggers = ref ecsWorld.GetPool<Triggers>().Add(entity.index);
            ref var collisions = ref ecsWorld.GetPool<Collisions>().Add(entity.index);
            ref var health = ref ecsWorld.GetPool<Health>().Add(entity.index);
            ref var impactDamage = ref ecsWorld.GetPool<ImpactDamage>().Add(entity.index);
            ref var spawner = ref ecsWorld.GetPool<Spawner>().Add(entity.index);
            ref var controller = ref ecsWorld.GetPool<BugController>().Add(entity.index);

            ref var inventory = ref ecsWorld.GetPool<InventoryComponent>().Add(entity.index);
            inventory.items = new List<Entity>();
            ecsWorld.GetPool<FireIfTarget>().Add(entity.index);
            
            ecsWorld.GetPool<TargetingAtPlayer>().Add(entity.index);
            ecsWorld.GetPool<DirectionAtTarget>().Add(entity.index);
            ecsWorld.GetPool<RotateToDirection>().Add(entity.index);
            ecsWorld.GetPool<VelocityByDirection>().Add(entity.index);
            ecsWorld.GetPool<SpawnIfDead>().Add(entity.index);


            
            
            body.value = GetComponent<Rigidbody2D>();
            animator.value = GetComponent<Animator>();
            controller.damage = damage;
            speed.value = this.speed;
            animationSpeed.coefficient = 1;
            triggers.entities = new HashSet<Entity>();
            collisions.entities = new HashSet<Entity>();
            health.maxValue = hp;
            health.value = hp;
            impactDamage.hits = new List<float>();
            spawner.target = DeadBlob;
        }

        private void OnTriggerEnter2D(Collider2D other) =>
            OnTriggerEnter2DAdapter(other);

        private void OnTriggerExit2D(Collider2D other) =>
            OnTriggerExit2DAdapter(other);

        private void OnCollisionEnter2D(Collision2D col)
        {
            OnCollisionEnter2DAdapter(col);
        }
        private void OnCollisionExit2D(Collision2D col)
        {
            OnCollisionExit2DAdapter(col);
        }
    }
}
