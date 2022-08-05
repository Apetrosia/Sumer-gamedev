using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    public class DistributorComponentSystem : IEcsRunSystem
    {
        readonly EcsWorldInject ecsWorld = default;
        readonly EcsFilterInject<Inc<DistributorComponent, Triggers>> filter = default;
        readonly EcsPoolInject<Triggers> triggersPool = default;
        readonly EcsPoolInject<DistributorComponent> controllerPool = default;
        readonly EcsPoolInject<InventoryComponent> inventoryPool = default; // playerTag => Inventory
        readonly EcsPoolInject<DeadTag> deadPool = default;
        readonly EcsPoolInject<Parent> parentPool = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in filter.Value)
            {
                ref var triggers = ref triggersPool.Value.Get(entity); 
                ref var controller = ref controllerPool.Value.Get(entity);
                if (triggers.entities.Count == 0) continue; // ни с чем не коллизируем
                foreach (var trigger in triggers.entities)
                {
                    if (!trigger.Alive(ecsWorld.Value)) continue;
                    if (!inventoryPool.Value.Has(trigger.index)) continue;
                    ref var inventory = ref inventoryPool.Value.Get(trigger.index);
                    inventory.items.Add(controller.item);
                    if (!deadPool.Value.Has(entity))
                        deadPool.Value.Add(entity); // suicide
                    if (parentPool.Value.Has(controller.item.index))
                        parentPool.Value.Del(controller.item.index);
                    break;
                }
            }   
        }
    }
}