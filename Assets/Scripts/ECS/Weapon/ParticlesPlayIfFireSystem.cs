using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    sealed class ParticlesPlayIfFireSystem : IEcsRunSystem
    {
        readonly EcsFilterInject<Inc<ParticlesPlayIfFire, ParticlesComponent>> filter = default;

        readonly EcsPoolInject<ParticlesComponent> particlesPool = default;
        readonly EcsPoolInject<FireTag> firePool = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in filter.Value)
            {
                ref var particles = ref particlesPool.Value.Get(entity);
                bool hasFire = firePool.Value.Has(entity);

                if (hasFire && particles.value.isStopped) particles.value.Play();
                if (!hasFire && particles.value.isPlaying) particles.value.Stop();
            }
        }
    }
}
