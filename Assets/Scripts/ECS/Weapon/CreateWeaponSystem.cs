using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    sealed class CreateWeaponSystem : IEcsRunSystem
    {
        readonly EcsWorldInject ecsWorld = default;
        readonly EcsFilterInject<Inc<CreateWeapon>> filter = default;

        readonly EcsPoolInject<CreateWeapon> createWeaponPool = default;
        readonly EcsPoolInject<SetWeapon> setWeaponPool = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in filter.Value)
            {
                ref var created = ref createWeaponPool.Value.Get(entity);

                GameObject weapon = Object.Instantiate(created.weapon);
                createWeaponPool.Value.Del(entity);

                var unitInitializer = weapon.GetComponent<EcsMonoBehaviour>();
                if (unitInitializer == null)
                {
                    Debug.LogWarning($"Weapon {created.weapon} has not {typeof(EcsMonoBehaviour)}!!");
                    Object.Destroy(weapon.gameObject);
                    continue;
                }
                unitInitializer.InitEntity(ecsWorld.Value);

                if (!setWeaponPool.Value.Has(entity))
                    setWeaponPool.Value.Add(entity);
                ref var setWeapon = ref setWeaponPool.Value.Get(entity);
                setWeapon.weapon = unitInitializer.entity;
            }
        }
    }
}
