using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    sealed class SpawnSystem: IEcsRunSystem
    {
        readonly EcsWorldInject ecsWorld = default;
        readonly EcsFilterInject<Inc<Spawner, SpawnEvent, TransformComponent>> filter = default;

        readonly EcsPoolInject<Spawner> spawnerPool = default;
        readonly EcsPoolInject<SpawnEvent> spawnEventPool = default;
        readonly EcsPoolInject<TransformComponent> transformPool = default;


        public void Run(IEcsSystems systems)
        {
            foreach (var entity in filter.Value)
            {
                spawnEventPool.Value.Del(entity);

                ref var spawner = ref spawnerPool.Value.Get(entity);
                ref var transform = ref transformPool.Value.Get(entity);
                var pos = transform.value.position;
                var rot = transform.value.rotation;
                var delta = Random.insideUnitCircle * Random.value * spawner.radius;

                GameObject unit = Object.Instantiate(
                    spawner.target,
                    pos + (Vector3)delta + (Vector3)(rot * spawner.offset),
                    rot
                );
                var initializer = unit.GetComponent<EcsMonoBehaviour>();
                if (initializer != null)
                    initializer.InitEntity(ecsWorld.Value);
            }
        }
    }
}