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
        public FixedJoystick joystick; // Movement JS
        public FixedJoystick joystick1; //Shooting JS

        public override void InitEntity(EcsWorld ecsWorld)
        {
            base.InitEntity(ecsWorld); // ref var velocity = ref ecsWorld.GetPool<VelocityByKeyboard>().Add(entity.index);

            joystick = GameObject.FindGameObjectWithTag("MovementJS").GetComponent<FixedJoystick>();
            joystick1 = GameObject.FindGameObjectWithTag("ShootingJS").GetComponent<FixedJoystick>();

            ref var body = ref ecsWorld.GetPool<BodyComponent>().Add(entity.index);
#if !(UNITY_ANDROID)
            ecsWorld.GetPool<VelocityByKeyboard>().Add(entity.index); // создаем компонент
            ecsWorld.GetPool<DirectionByMouse>().Add(entity.index);
            ecsWorld.GetPool<FireByMouse>().Add(entity.index);
            GameObject.FindGameObjectWithTag("MovementJS").SetActive(false);
            GameObject.FindGameObjectWithTag("ShootingJS").SetActive(false);
#endif
#if (UNITY_ANDROID || UNITY_EDITOR)
            ref var js = ref ecsWorld.GetPool<JoystickMovementComponent>().Add(entity.index);
            ref var js1 = ref ecsWorld.GetPool<JoystickShooting>().Add(entity.index);
            ecsWorld.GetPool<FireByJoystick>().Add(entity.index);
            js.joystick = joystick;
            js1.joystick = joystick1;
#endif
            ref var speed = ref ecsWorld.GetPool<Speed>().Add(entity.index);
            ref var createWeapon = ref ecsWorld.GetPool<CreateWeapon>().Add(entity.index);
            ref var health = ref ecsWorld.GetPool<Health>().Add(entity.index);
            ref var impactDamage = ref ecsWorld.GetPool<ImpactDamage>().Add(entity.index);
            ecsWorld.GetPool<RotateToDirection>().Add(entity.index);
            ecsWorld.GetPool<PlayerTag>().Add(entity.index);
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
