using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    sealed class FireByJoystickSystem : IEcsRunSystem
    {
        readonly EcsWorldInject ecsWorld = default;
        readonly EcsFilterInject<Inc<FireByJoystick, JoystickShooting, Weapon>> filter = default;
        readonly EcsPoolInject<JoystickShooting> joystickPool = default;
        readonly EcsPoolInject<Weapon> weaponPool = default;
        readonly EcsPoolInject<FireTag> firePool = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in filter.Value)
            {
                ref var js = ref joystickPool.Value.Get(entity);
                if (js.joystick.Horizontal == 0 && js.joystick.Vertical == 0) continue;
                ref var weapon = ref weaponPool.Value.Get(entity);
                if (!weapon.value.Alive(ecsWorld.Value)) continue;
                if (firePool.Value.Has(weapon.value.index)) continue;
                firePool.Value.Add(weapon.value.index);
            }
        }
    }
}
