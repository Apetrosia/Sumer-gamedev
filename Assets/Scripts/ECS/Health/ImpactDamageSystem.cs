using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    sealed class ImpactDamageSystem : IEcsRunSystem
    {
        readonly EcsFilterInject<Inc<Health, ImpactDamage>> hitFilter = default;
        
        readonly EcsPoolInject<Health> healthPool = default;
        readonly EcsPoolInject<ImpactDamage> impactPool = default;

        public void Run(IEcsSystems systems)
        {
            //Снимаем все тэги получения урона
            foreach(var entity in hitFilter.Value)
            {
                
                ref var health = ref healthPool.Value.Get(entity);
                ref var impact = ref impactPool.Value.Get(entity);
                foreach(var hit in impact.hits)
                    health.value -= hit;
                    //Вешаем тег получн=ения урона
                impact.hits.Clear();
            }
        }
    }
}
