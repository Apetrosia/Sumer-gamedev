using System.Collections.Generic;
using Leopotam.EcsLite;
using Unity.VisualScripting;
using UnityEngine;

namespace Client
{
    public class Distributor : EcsMonoBehaviour
    {
        public GameObject itemPrefab;

        public override void InitEntity(EcsWorld ecsWorld)
        {
            base.InitEntity(ecsWorld);
            
            ref var triggers = ref ecsWorld.GetPool<Triggers>().Add(entity.index);
            triggers.entities = new HashSet<Entity>();
            ref var controller = ref ecsWorld.GetPool<DistributorComponent>().Add(entity.index);
            GameObject item = Object.Instantiate(
                itemPrefab
            );
            var initializer = item.GetComponent<EcsMonoBehaviour>();
            if (initializer != null)
                initializer.InitEntity(ecsWorld);
            controller.item = initializer.entity;
            ref var itemParent = ref ecsWorld.GetPool<Parent>().Add(controller.item.index);
            itemParent.value = entity;
        }
        
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.isTrigger) return;
            OnTriggerEnter2DAdapter(other);
        }

        private void OnTriggerExit2D(Collider2D other) =>
            OnTriggerExit2DAdapter(other);

    }
}