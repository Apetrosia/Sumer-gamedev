using System.Collections;
using System.Collections.Generic;
using Leopotam.EcsLite;
using UnityEngine;


namespace Client
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Player : EcsMonoBehaviour
    {
        public float hp = 10;
        public float speed = 5;
        public GameObject defaultWeapon;

        public override void InitEntity(EcsWorld ecsWorld)
        {
            base.InitEntity(ecsWorld);

            ref var body = ref ecsWorld.GetPool<BodyComponent>().Add(entity.index);
            ecsWorld.GetPool<VelocityByKeyboard>().Add(entity.index); // создаем компонент
            ref var speed = ref ecsWorld.GetPool<Speed>().Add(entity.index);
            ref var createWeapon = ref ecsWorld.GetPool<CreateWeapon>().Add(entity.index);
            ref var health = ref ecsWorld.GetPool<Health>().Add(entity.index);
            ref var impactDamage = ref ecsWorld.GetPool<ImpactDamage>().Add(entity.index);
            ecsWorld.GetPool<DirectionByMouse>().Add(entity.index);
            ecsWorld.GetPool<RotateToDirection>().Add(entity.index);
            ecsWorld.GetPool<PlayerTag>().Add(entity.index);
            ecsWorld.GetPool<FireByMouse>().Add(entity.index);
            ref var inventory = ref ecsWorld.GetPool<InventoryComponent>().Add(entity.index);

            
            body.value = GetComponent<Rigidbody2D>();
            speed.value = this.speed;
            createWeapon.weapon = defaultWeapon;
            health.value = hp;
            health.maxValue = hp;
            impactDamage.hits = new List<float>();
            inventory.items = new List<Entity>();
        }
    }
}
