using System.Collections;
using System.Collections.Generic;
using Leopotam.EcsLite;
using UnityEngine;


namespace Client
{
    [RequireComponent(typeof(Animator))]
    public class Gun: EcsMonoBehaviour
    {
        public GameObject bullet;
        public override void InitEntity(EcsWorld ecsWorld)
        {
            base.InitEntity(ecsWorld);

            ref var animatorComponent = ref ecsWorld.GetPool<AnimatorComponent>().Add(entity.index);
            ref var spawner = ref ecsWorld.GetPool<Spawner>().Add(entity.index);
            ecsWorld.GetPool<RotationLikeParent>().Add(entity.index);
            ecsWorld.GetPool<PositionLikeParent>().Add(entity.index);
            ecsWorld.GetPool<DeadLikeParent>().Add(entity.index);
            ecsWorld.GetPool<FireAutoClear>().Add(entity.index);
            ecsWorld.GetPool<GunController>().Add(entity.index);

            animatorComponent.value = GetComponent<Animator>();
            spawner.target = bullet;
            spawner.offset = new Vector2(0, 1);
        }

        public void AnimationFire() {
            var spawnEventPool = ecsWorld.GetPool<SpawnEvent>();
             if (spawnEventPool.Has(entity.index)) return;
                spawnEventPool.Add(entity.index);
            
        }
    }
}
