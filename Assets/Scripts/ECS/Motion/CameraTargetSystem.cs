using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    sealed class CameraTargetSystem : IEcsRunSystem
    {
        readonly EcsWorldInject ecsWorld = default;
        readonly EcsFilterInject<Inc<CameraTarget, TransformComponent>> target_filter = default;
        readonly EcsFilterInject<Inc<CameraController>> camera_filter = default;

        readonly EcsPoolInject<Parent> parentPool = default;
        readonly EcsPoolInject<CameraTarget> targetPool = default;
        readonly EcsPoolInject<CameraController> cameraPool = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in camera_filter.Value)
            {
                ref var camera = ref cameraPool.Value.Get(entity);
                if (!parentPool.Value.Has(entity)){
                    parentPool.Value.Add(entity);
                }
                ref var cameraParent = ref parentPool.Value.Get(entity);
                if (cameraParent.value.Alive(ecsWorld.Value)) continue;
                foreach (var target_entity in target_filter.Value){
                    ref var target = ref targetPool.Value.Get(target_entity);
                    cameraParent.value = new Entity() { index = target_entity, gen = ecsWorld.Value.GetEntityGen(target_entity) };
                }
            }
        }
    }
}