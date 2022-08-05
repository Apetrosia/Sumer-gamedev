using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    sealed class FireByMouseSystem: IEcsRunSystem
    {
        readonly EcsWorldInject ecsWorld = default;
        readonly EcsFilterInject<Inc<FireByMouse, Weapon>> filter = default;

        readonly EcsPoolInject<Weapon> weaponPool = default;
        readonly EcsPoolInject<FireTag> firePool = default;

        public void Run(IEcsSystems systems)
        {
            if (!Input.GetMouseButton(0)) return;
            foreach (var entity in filter.Value)
            {
                ref var weapon = ref weaponPool.Value.Get(entity);
                if (!weapon.value.Alive(ecsWorld.Value)) continue;
                if (firePool.Value.Has(weapon.value.index)) continue;
                firePool.Value.Add(weapon.value.index);
            }
        }
    }
}
