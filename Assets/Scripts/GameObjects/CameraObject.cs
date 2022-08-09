using System.Collections;
using System.Collections.Generic;
using Leopotam.EcsLite;
using UnityEngine;


namespace Client
{
    public class CameraObject: EcsMonoBehaviour
    {
        public override void InitEntity(EcsWorld ecsWorld)
        {
            base.InitEntity(ecsWorld);

            ecsWorld.GetPool<CameraController>().Add(entity.index);
            ecsWorld.GetPool<RotationLikeParent>().Add(entity.index);
            ecsWorld.GetPool<PositionLikeParent>().Add(entity.index);
        }
    }
}
