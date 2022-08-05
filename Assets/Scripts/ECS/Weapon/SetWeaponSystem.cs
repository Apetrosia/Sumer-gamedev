using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    sealed class SetWeaponSystem : IEcsRunSystem
    {
        readonly EcsWorldInject ecsWorld = default;
        readonly EcsFilterInject<Inc<SetWeapon>> filter = default;

        readonly EcsPoolInject<SetWeapon> setWeaponPool = default;
        readonly EcsPoolInject<Weapon> weaponPool = default;
        readonly EcsPoolInject<Parent> parentPool = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in filter.Value)
            {
                ref var added = ref setWeaponPool.Value.Get(entity);
                if (!added.weapon.Alive(ecsWorld.Value)) continue;

                if (!weaponPool.Value.Has(entity))
                    weaponPool.Value.Add(entity);
                ref var weapon = ref weaponPool.Value.Get(entity);
                weapon.value = added.weapon;

                if (!parentPool.Value.Has(weapon.value.index))
                    parentPool.Value.Add(weapon.value.index);
                ref var weaponParent = ref parentPool.Value.Get(weapon.value.index);
                weaponParent.value = new Entity() { index = entity, gen = ecsWorld.Value.GetEntityGen(entity) };

                setWeaponPool.Value.Del(entity);
            }
        }
    }
}
