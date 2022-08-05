using System.Collections;
using System.Collections.Generic;
using Leopotam.EcsLite;
using UnityEngine;


namespace Client
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Bullet : EcsMonoBehaviour
    {
        public float damage = 1;
        public float speed = 1;

        public override void InitEntity(EcsWorld ecsWorld)
        {
            base.InitEntity(ecsWorld);

            ref var triggers = ref ecsWorld.GetPool<Triggers>().Add(entity.index);
            ref var body = ref ecsWorld.GetPool<BodyComponent>().Add(entity.index);
            ref var controller = ref ecsWorld.GetPool<BulletController>().Add(entity.index);
            ref var speedComponent = ref ecsWorld.GetPool<Speed>().Add(entity.index);
            ref var direction = ref ecsWorld.GetPool<Direction>().Add(entity.index);
            ecsWorld.GetPool<VelocityByDirection>().Add(entity.index);

            triggers.entities = new HashSet<Entity>();
            body.value = GetComponent<Rigidbody2D>();
            controller.damage = damage;
            speedComponent.value = speed;
            direction.value = transform.rotation * new Vector2(0, 1);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.isTrigger) return;
            OnTriggerEnter2DAdapter(other);
        }

        private void OnTriggerExit2D(Collider2D other) =>
            OnTriggerExit2DAdapter(other);

    }
}
