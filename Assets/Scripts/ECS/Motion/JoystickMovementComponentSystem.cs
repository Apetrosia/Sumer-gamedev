using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using System;

namespace Client
{
    class JoystickMovementComponentSystem : IEcsRunSystem
    {
        readonly EcsWorldInject ecsWorld = default;
        readonly EcsFilterInject<Inc<JoystickMovementComponent, BodyComponent, Speed>> filter = default;
        readonly EcsPoolInject<BodyComponent> bodyPool = default;
        readonly EcsPoolInject<Speed> speedPool = default;
        readonly EcsPoolInject<JoystickMovementComponent> joystickPool = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in filter.Value)
            {
                ref var joystick = ref joystickPool.Value.Get(entity);
                var direction = new Vector2(joystick.joystick.Horizontal, joystick.joystick.Vertical);
                direction.Normalize();
                ref var body = ref bodyPool.Value.Get(entity);
                ref var speed = ref speedPool.Value.Get(entity);
                body.value.velocity = direction * speed.value;
            }
        }
    }
}
