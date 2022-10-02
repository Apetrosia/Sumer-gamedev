using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    public class JoystickShootingSystem : IEcsRunSystem
    {
        readonly EcsWorldInject ecsWorld = default;
        readonly EcsFilterInject<Inc<JoystickShooting, PlayerTag>> filter = default;
        readonly EcsPoolInject<JoystickShooting> jsShootingPool = default;
        readonly EcsPoolInject<Direction> directionPool = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in filter.Value)
            {
                //Debug.Log(entity);
                //System.Type[] types = new System.Type[10];
                //var count = ecsWorld.Value.GetComponentTypes(entity, ref types);
                //if (count > 0)
                //{
                //    Debug.Log(count);
                //    foreach (var type in types) Debug.Log(type);
                //}
                ref var joystick = ref jsShootingPool.Value.Get(entity);
                var delta = new Vector2(joystick.joystick.Horizontal, joystick.joystick.Vertical);
                delta.Normalize();
                if (!directionPool.Value.Has(entity))
                    directionPool.Value.Add(entity);
                ref var direction = ref directionPool.Value.Get(entity);
                direction.value = delta;
            }
        }
    }
}
