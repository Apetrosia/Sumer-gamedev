using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    sealed class VelocityByKeyboardSystem : IEcsRunSystem
    {
        readonly EcsFilterInject<Inc<BodyComponent, VelocityByKeyboard, Speed>> filter = default;

        readonly EcsPoolInject<BodyComponent> bodyPool = default;
        readonly EcsPoolInject<VelocityByKeyboard> keyControlPool = default;
        readonly EcsPoolInject<Speed> speedPool = default;

        public void Run(IEcsSystems systems)
        {
            var vInput = Input.GetAxisRaw("Vertical");
            var hInput = Input.GetAxisRaw("Horizontal");
            var direction = new Vector2(hInput, vInput);
            direction.Normalize();

            foreach (var entity in filter.Value)
            {
                ref var body = ref bodyPool.Value.Get(entity);
                ref var control = ref keyControlPool.Value.Get(entity);
                ref var speed = ref speedPool.Value.Get(entity);
                body.value.velocity = direction * speed.value;
            }
        }
    }
}