using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    sealed class AnimationSpeedByVelocitySystem: IEcsRunSystem
    {
        readonly EcsFilterInject<Inc<AnimationSpeedByVelocity, BodyComponent, AnimatorComponent>> filter = default;

        readonly EcsPoolInject<AnimatorComponent> animatorPool = default;
        readonly EcsPoolInject<BodyComponent> bodyPool = default;
        readonly EcsPoolInject<AnimationSpeedByVelocity> controllerPool = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in filter.Value)
            {
                ref var animator = ref animatorPool.Value.Get(entity);
                ref var body = ref bodyPool.Value.Get(entity);
                ref var controller = ref controllerPool.Value.Get(entity);
                animator.value.speed = controller.coefficient * body.value.velocity.magnitude;
            }
        }
    }
}