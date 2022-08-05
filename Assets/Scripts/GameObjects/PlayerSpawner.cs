using System.Collections;
using System.Collections.Generic;
using Leopotam.EcsLite;
using UnityEngine;


namespace Client
{
    public class PlayerSpawner : EcsMonoBehaviour
    {
        public float spawnRadius = 1;
        public GameObject player;

        public override void InitEntity(EcsWorld ecsWorld)
        {
            base.InitEntity(ecsWorld);

            ref var spawner = ref ecsWorld.GetPool<Spawner>().Add(entity.index);
            ecsWorld.GetPool<PlayerSpawnerController>().Add(entity.index);

            spawner.target = player;
            spawner.radius = spawnRadius;
        }
    }
}
