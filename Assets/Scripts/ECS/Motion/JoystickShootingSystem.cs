using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using System;

namespace Client
{
    internal class JoystickShootingSystem : IEcsRunSystem
    {
        readonly EcsWorld ecsWorld = default;
        // readonly EcsCustomInject<SceneData> sceneData = default;
        readonly EcsFilterInject<Inc<JoystickShooting, PlayerTag>> filter = default;
        readonly EcsPoolInject<JoystickShooting> jsShootingPool = default;
        readonly EcsPoolInject<Direction> directionPool = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in filter.Value)
            {
                ref var js = ref jsShootingPool.Value.Get(entity);
                var delta = new Vector2(js.joystick.Horizontal, js.joystick.Vertical);
                delta.Normalize();
                if (!directionPool.Value.Has(entity))
                    directionPool.Value.Add(entity);
                ref var direction = ref directionPool.Value.Get(entity);
                direction.value = delta;
            }
        }
    }
}
