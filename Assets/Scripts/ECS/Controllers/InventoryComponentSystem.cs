using System.Linq;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    public class InventoryComponentSystem : IEcsRunSystem
    {
        readonly EcsWorldInject ecsWorld = default;
        readonly EcsPoolInject<SetWeapon> setWeaponPool = default;
        readonly EcsPoolInject<InventoryComponent> inventoryPool = default;
        readonly EcsPoolInject<Weapon> weaponPool = default;
        readonly EcsPoolInject<DeadTag> deadPool = default;
        readonly EcsFilterInject<Inc<InventoryComponent>> filter = default;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in filter.Value)
            {
                ref var inventory = ref inventoryPool.Value.Get(entity);
                if (inventory.items.Count == 0) continue;
                ref var setWeapon = ref setWeaponPool.Value.Add(entity);
                setWeapon.weapon = inventory.items.First();
                inventory.items.Clear();

                if (!weaponPool.Value.Has(entity)) continue;
                ref var weapon = ref weaponPool.Value.Get(entity);
                deadPool.Value.Add(weapon.value.index);
            }
        }
    }
}