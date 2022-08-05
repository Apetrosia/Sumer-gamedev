using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    sealed class FireAutoClearSystem : IEcsRunSystem
    {
        readonly EcsFilterInject<Inc<FireTag>> filter = default;
        readonly EcsPoolInject<FireTag> firePool = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in filter.Value)
            {
                firePool.Value.Del(entity);
            }
        }
    }
}
