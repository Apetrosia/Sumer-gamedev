using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    sealed class TargetingAtPlayerSystem : IEcsRunSystem
    {
        readonly EcsWorldInject ecsWorld = default;
        readonly EcsFilterInject<Inc<TargetingAtPlayer, Triggers>> filter = default;
        readonly EcsFilterInject<Inc<PlayerTag>> playersFilter = default;

        readonly EcsPoolInject<Target> targetsPool = default;
        readonly EcsPoolInject<Triggers> triggersPool = default;


        public void Run(IEcsSystems systems)
        {
            foreach (var entity in filter.Value)
            {
                ref var triggers = ref triggersPool.Value.Get(entity);

                bool hasPlayer = false;
                foreach (var player in playersFilter.Value)
                {
                    var playerEntity = new Entity() {
                        index = player, gen = ecsWorld.Value.GetEntityGen(player)};
                    if (!triggers.entities.Contains(playerEntity)) continue;
                    if (!targetsPool.Value.Has(entity)) targetsPool.Value.Add(entity);
                    ref var target = ref targetsPool.Value.Get(entity);
                    target.value = playerEntity;
                    hasPlayer = true;
                }
                if (hasPlayer) continue;
                if (targetsPool.Value.Has(entity))
                    targetsPool.Value.Del(entity);
            }
        }
    }
}
