using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    sealed class DirectionByMouseSystem : IEcsRunSystem
    {
        readonly EcsCustomInject<SceneData> sceneData = default;
        readonly EcsFilterInject<Inc<DirectionByMouse, TransformComponent>> filter = default;

        readonly EcsPoolInject<Direction> directionPool = default;
        readonly EcsPoolInject<TransformComponent> transformPool = default;

        public void Run(IEcsSystems systems)
        {
            Vector3 screenMousePos = Input.mousePosition;
            var mousePos = (Vector2)sceneData.Value.mainCamera.ScreenToWorldPoint(screenMousePos);
            foreach (var entity in filter.Value)
            {
                ref var transform = ref transformPool.Value.Get(entity);
                var delta = mousePos - (Vector2)transform.value.position;
                delta.Normalize();
                if (!directionPool.Value.Has(entity))
                    directionPool.Value.Add(entity);
                ref var direction = ref directionPool.Value.Get(entity);
                direction.value = delta;
            }
        }
    }
}
