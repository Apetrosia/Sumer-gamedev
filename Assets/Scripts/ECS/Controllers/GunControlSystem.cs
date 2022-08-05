using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    sealed class GunControlSystem : IEcsRunSystem
    {
        readonly EcsFilterInject<Inc<GunController, AnimatorComponent, FireTag>> fireFilter = default;
        readonly EcsFilterInject<Inc<GunController, AnimatorComponent>, Exc<FireTag>> notFireFilter = default;
        readonly EcsPoolInject<AnimatorComponent> animatorPool = default;
        readonly EcsPoolInject<FireTag> firePool = default;
        readonly EcsPoolInject<SpawnEvent> spawnEventPool = default;

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in fireFilter.Value)
            {
                ref var animator = ref animatorPool.Value.Get(entity);
                animator.value.SetBool("Load", true);
               }
            foreach (int entity in notFireFilter.Value)
            {
                ref var animator = ref animatorPool.Value.Get(entity);
                animator.value.SetBool("Load", false);
            }
        }
    }
}
